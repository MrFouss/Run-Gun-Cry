using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int Health;

    public int LaserDamage = 10;
    public int MissileDamage = 35;
    public int MechaCollisionDamage = 100;

    private AudioSource audioSource;

    public AudioClip HitByLaserSound;
    public AudioClip DestructionSound;

    public Animation HitByLaserAnimation;
    public Animation DestructionAnimation;



	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case Tags.MechaLaserTag:
                TakeDamage(LaserDamage);
                //HitByLaserAnimation.Play(); 
                // TODO uncomment when these files are added
                //audioSource.clip = HitByLaserSound;
                //audioSource.Play();
                break;
            case Tags.MechaMissileTag:
                TakeDamage(MissileDamage);
                break;
            case Tags.MechaBodyTag:
                TakeDamage(MechaCollisionDamage);
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
