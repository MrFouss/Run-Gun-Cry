using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour {

    [HideInInspector]
    protected BoxCollider boxCollider;
    [HideInInspector]
    protected Rigidbody rbd;

	// Use this for initialization
	void Start () {
       boxCollider = GetComponent<BoxCollider>();
       rbd = GetComponent<Rigidbody>(); 
	}
	
	// Update is called once per frame
	void Update () {

    }

    // collision with :
    // enemy projectiles - "enemy_laser"
    // void (mecha falling between platforms) - "void"
    // charging ennemies - "enemy_charger"
    // obstacles - "obstale_wall" - "obstacle_trap"
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger from" + collider.gameObject.tag);
        switch (collider.gameObject.tag)
        {
            case ("Enemy_Laser"):
                Debug.Log("shot by enemy laser");
                MechaResources.Mecha_DamageTaken(5);
                break;
            case ("Void"):
                Debug.Log("Fell in the void");
                MechaResources.Mecha_DamageTaken(30);
                break;
            case ("Enemy_Charger"):
                Debug.Log("Hit by charger");
                MechaResources.Mecha_DamageTaken(20);
                break;
            case ("Obstacle_Wall"):
                Debug.Log("Hit a wall");
                MechaResources.Mecha_DamageTaken(30);
                break;
            default:
                break;
        }
    }
}
