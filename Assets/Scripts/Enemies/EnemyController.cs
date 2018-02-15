﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour {

    public int Health;

    public int Damage = 10;

    // Delete when damages function works
    public int MissileDamage = 35;
    public int MechaCollisionDamage = 100;

    private AudioSource audioSource;

    public AudioClip HitByLaserSound;
    public AudioClip DestructionSound;

    public Animation HitByLaserAnimation;
    public Animation DestructionAnimation;



	// Use this for initialization
	protected void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;
    }
	
    protected void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case Tags.MechaLaserTag:
                TakeDamage(collision.gameObject.GetComponent<ProjectileBehavior>().Damage);
                //HitByLaserAnimation.Play(); 
                // TODO uncomment when these files are added
                //audioSource.clip = HitByLaserSound;
                //audioSource.Play();
                break;
            case Tags.MechaMissileTag:
                TakeDamage(collision.gameObject.GetComponent<CannonBehavior>().Damage);
                break;
            case Tags.MechaBodyTag:
                TakeDamage(collision.gameObject.GetComponent<MechaController>().Damage);
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            // TODO uncomment when these files are added
            //audioSource.clip = DestructionSound;
            //audioSource.Play();
            StartCoroutine("Death");
        }
    }

    IEnumerator Death()
    {
        // TODO uncomment when these files are added
        //DestructionAnimation.Play();
        //yield return DestructionAnimation.clip.length;

        Destroy(gameObject);

        yield return null;
    }
}
