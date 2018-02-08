using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public AudioClip DestroySound;

    public int FuseTime;

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
		transform.position += transform.forward * Time.deltaTime * 1000.0f;
	}
    
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(DestroySound, 1.0f);
        Explode();
    }

    void Explode()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        Destroy(gameObject, 0.25f);
    }

}
