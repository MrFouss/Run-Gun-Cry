using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEffectBehaviour : MonoBehaviour {

    public AudioClip[] Sounds;

    private float timeToLive;

	void Start () {
        AudioClip clip = Sounds[Random.Range(0,Sounds.Length)];
        AudioSource.PlayClipAtPoint(clip, transform.position, 10.0f);

        // Destroy after first particle effect loop
        timeToLive = gameObject.GetComponent<ParticleSystem>().main.duration - 0.1f;
        Destroy(gameObject, timeToLive);
	}
	
}
