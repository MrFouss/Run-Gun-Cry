using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public AudioClip DestroySound;
    public GameObject ExplosionParticleSystem;

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
    
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Tags.ObstacleWallTag:
            case Tags.EnemyChargerTag:
            case Tags.EnemyLaserTag:
                Explode();
                break;

            default:
                break;
        }
    }

    private void Explode()
    {
        audioSource.Stop();

        AudioSource.PlayClipAtPoint(DestroySound, transform.position, 1.0f);
        Destroy(Instantiate(ExplosionParticleSystem, transform.position, transform.rotation), 1.0f);
        Destroy(gameObject);
    }

}
