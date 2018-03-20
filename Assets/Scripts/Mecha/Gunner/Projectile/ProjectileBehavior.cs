using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public AudioClip DestroySound;
    public GameObject ExplosionParticleSystem;

    public enum ProjectileSource { MECHA, ENEMY_LASER };

    public ProjectileSource Source;
    public float ProjectileSpeed = 1000.0f;
    public float FuseTime = 2.0f;
    public int Damage = 10;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        Destroy(Instantiate(ExplosionParticleSystem, transform.position, transform.rotation), 1.0f);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ProjectileSpeed);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ShotSound;
        audioSource.Play();

        Invoke("Explode", FuseTime); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Source == ProjectileSource.MECHA) {
            // when a projectile shot from mecha hits an enemy or an obstacle, create an event for the score to calculate accuracy later on
            if (collision.gameObject.tag == Tags.EnemyChargerTag || collision.gameObject.tag == Tags.ObstacleWallTag) {
                if (gameObject.tag == Tags.MechaLaserTag) {
                    EventManager.onShotHitting.Invoke(ShotType.Laser);
                }
                else if (gameObject.tag == Tags.MechaMissileTag) {
                    EventManager.onShotHitting.Invoke(ShotType.Missile);
                }
            }
            switch (collision.gameObject.tag) {
                case Tags.ObstacleWallTag:
                case Tags.EnemyChargerTag:
                case Tags.EnemyLaserTag:
                    Explode();
                    break;

                default:
                    break;
            }

        } else if (Source == ProjectileSource.ENEMY_LASER) {

            switch (collision.gameObject.tag) {
                case Tags.MechaBodyTag:
                    Explode();
                    break;

                default:
                    break;
            }
        }
    }

    private void Explode()
    {
        audioSource.Stop();

        AudioSource.PlayClipAtPoint(DestroySound, transform.position, 1.0f);
        Destroy(Instantiate(ExplosionParticleSystem, transform.position, transform.rotation), 1.0f);
        Destroy(gameObject, 0.1f);
    }

}
