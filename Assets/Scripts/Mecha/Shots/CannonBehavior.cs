using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform muzzle;
	public GameObject laser_shot;
    public GameObject missile_shot;
    public float fire_rate_laser;
    public float fire_rate_missile;

    private float next_fire;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (Input.GetKey("mouse 0") && Time.time > next_fire)
		{
            next_fire = Time.time + fire_rate_laser;
			GameObject go = GameObject.Instantiate(laser_shot, muzzle.position, muzzle.rotation) as GameObject;
			//GameObject.Destroy(go, 3f);
		}

        if (Input.GetKey("mouse 1") && Time.time > next_fire)
        {
            next_fire = Time.time + fire_rate_missile;
            GameObject go = GameObject.Instantiate(missile_shot, muzzle.position, muzzle.rotation) as GameObject;
            //GameObject.Destroy(go, 3f);
        }

    }	
}
