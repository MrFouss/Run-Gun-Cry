using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha : MonoBehaviour
{
    // resources
    public int health;
    public int shield;
    public int energy;

    private bool isDead;
    private bool isShieldEmpty;
    private bool isEnergyEmpty;

    // collisions
    private BoxCollider boxCollider;
    private Rigidbody rbd;

    // sound
    private AudioSource audioSource;

    private AudioClip audioClip_Collision_Enemy_Laser;
    private AudioClip audioClip_Collision_Void;
    private AudioClip audioClip_Collision_Enemy_Charger;
    private AudioClip audioClip_Collision_Obstacle_Wall;


    // animation
    //TODO : uncomment when animations are available
    //private Animation animation_Collision_Enemy_Laser;
    //private Animation animation_Collision_Void;
    //private Animation animation_Collision_Enemy_Charger;
    //private Animation animation_Collision_Obstacle_Wall;



    // Use this for initialization
    void Start()
    {
        // initial resources
        health = 100;
        shield = 100;
        energy = 75;

        isDead = false;
        isShieldEmpty = false;
        isEnergyEmpty = false;

        // get colliders
        boxCollider = GetComponent<BoxCollider>();
        rbd = GetComponent<Rigidbody>();

        // set AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;

        //TODO : uncomment when available
        // Get Audioclips
        //audioClip_Enemy_Laser = Resources.Load("Sounds/Feedbacks/") as AudioClip;
        //audioClip_Void = Resources.Load("Sounds/Feedbacks/") as AudioClip;
        //audioClip_Enemy_Charger = Resources.Load("Sounds/Feedbacks/") as AudioClip;
        //audioClip_Obstacle_Wall = Resources.Load("Sounds/Feedbacks/") as AudioClip;

        //TODO : uncomment when available
        // Get Animations
        //animation_Enemy_Laser = Resources.Load("Animations/") as Animation;
        //animation_Void = Resources.Load("Animations/") as Animation;
        //animation_Enemy_Charger = Resources.Load("Animations/") as Animation;
        //animation_Obstacle_Wall = Resources.Load("Animations/") as Animation;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Mecha is Destoyed");
        }

        if (shield <= 0)
        {
            isShieldEmpty = true;
        }
        else
        {
            isShieldEmpty = false;
        }

        if (energy <= 0)
        {
            isEnergyEmpty = true;
        }
        else
        {
            isEnergyEmpty = false;
        }
    }

    public void Mecha_DamageTaken(int damage)
    {
        if (isShieldEmpty)
        {
            health -= damage;
        }
        else
        {
            if (damage - shield > 0)
            {
                int remainingDamage = damage - shield;
                shield = 0;
                health -= remainingDamage;
            }
            else
            {
                shield -= damage;
            }

        }
        Debug.Log("Health : " + health + "\n" + "Shield : " + shield);
    }

    // TODO : uncomment when available
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
            case ("EnemyLaser"):
                Debug.Log("shot by enemy laser");

                audioSource.clip = audioClip_Collision_Enemy_Laser;
                audioSource.Play();

                //animation_Enemy_Laser.Play();
                Mecha_DamageTaken(5);
                break;
            case ("Void"):
                Debug.Log("Fell in the void");

                audioSource.clip = audioClip_Collision_Void;
                audioSource.Play();

                //animation_Void.Play();
                Mecha_DamageTaken(30);
                break;
            case ("EnemyCharger"):
                Debug.Log("Hit by charger");

                audioSource.clip = audioClip_Collision_Enemy_Charger;
                audioSource.Play();

                //animation_Enemy_Charger.Play();
                Mecha_DamageTaken(20);
                break;
            case ("ObstacleWall"):
                Debug.Log("Hit a wall");

                audioSource.clip = audioClip_Collision_Obstacle_Wall;
                audioSource.Play();

                //animation_Obstacle_Wall.Play();
                Mecha_DamageTaken(30);
                break;
            default:
                break;
        }
    }
}
