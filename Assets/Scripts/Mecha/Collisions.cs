using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour {

    [HideInInspector]
    private BoxCollider boxCollider;
    [HideInInspector]
    private Rigidbody rbd;
    [HideInInspector]
    private AudioSource audioSource;

    private AudioClip audioClip_Enemy_Laser;
    private AudioClip audioClip_Void;
    private AudioClip audioClip_Enemy_Charger;
    private AudioClip audioClip_Obstacle_Wall;

    private Animation animation_Enemy_Laser;
    private Animation animation_Void;
    private Animation animation_Enemy_Charger;
    private Animation animation_Obstacle_Wall;



    // Use this for initialization
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        rbd = GetComponent<Rigidbody>();

        // set AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;

        // Get Audioclips
        //audioClip_Enemy_Laser = Resources.Load("Sounds/Feedbacks/") as AudioClip;
        //audioClip_Void = Resources.Load("Sounds/Feedbacks/") as AudioClip;
        //audioClip_Enemy_Charger = Resources.Load("Sounds/Feedbacks/") as AudioClip;
        //audioClip_Obstacle_Wall = Resources.Load("Sounds/Feedbacks/") as AudioClip;

        // Get Animations
        //animation_Enemy_Laser = Resources.Load("Animations/") as Animation;
        //animation_Void = Resources.Load("Animations/") as Animation;
        //animation_Enemy_Charger = Resources.Load("Animations/") as Animation;
        //animation_Obstacle_Wall = Resources.Load("Animations/") as Animation;

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

                audioSource.clip = audioClip_Enemy_Laser;
                audioSource.Play();

                //animation_Enemy_Laser.Play();
                MechaResources.Mecha_DamageTaken(5);
                break;
            case ("Void"):
                Debug.Log("Fell in the void");

                audioSource.clip = audioClip_Void;
                audioSource.Play();

                //animation_Void.Play();
                MechaResources.Mecha_DamageTaken(30);
                break;
            case ("Enemy_Charger"):
                Debug.Log("Hit by charger");

                audioSource.clip = audioClip_Enemy_Charger;
                audioSource.Play();

                //animation_Enemy_Charger.Play();
                MechaResources.Mecha_DamageTaken(20);
                break;
            case ("Obstacle_Wall"):
                Debug.Log("Hit a wall");

                audioSource.clip = audioClip_Obstacle_Wall;
                audioSource.Play();

                //animation_Obstacle_Wall.Play();
                MechaResources.Mecha_DamageTaken(30);
                break;
            default:
                break;
        }
    }
}
