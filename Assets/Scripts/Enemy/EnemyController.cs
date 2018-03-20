using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int Health = 100;

    public int Damage = 10;

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
            case Tags.ObstacleWallTag:
                TakeDamage(Health / 2);
                break;
            case Tags.MechaBodyTag:
                TakeDamage(Health);
                break;
            default:
                break;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Tags.MechaLaserTag:
                EventManager.onShotHitting.Invoke(ShotType.Laser);
                break;
            case Tags.MechaMissileTag:
                EventManager.onShotHitting.Invoke(ShotType.Missile);
                TakeDamage(other.gameObject.GetComponentInParent<ProjectileBehavior>().Damage);
                //HitByLaserAnimation.Play(); 
                // TODO uncomment when these files are added
                //audioSource.clip = HitByLaserSound;
                //audioSource.Play();
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
