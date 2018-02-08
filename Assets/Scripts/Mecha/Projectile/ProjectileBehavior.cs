using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public AudioClip DestroySound;
    public float ProjectileSpeed = 1000.0f;

    public float FuseTime = 1.0f;

    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        Invoke("Explode", FuseTime);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ShotSound;
        audioSource.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
		transform.position += transform.forward * Time.deltaTime * ProjectileSpeed;
	}
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(DestroySound, 1.0f);

        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();

        Destroy(gameObject, 0.25f);
    }

}
