using UnityEngine;
using System.Collections;

public class MissileBehavior : MonoBehaviour {

    public AudioClip shot_sound;
    public AudioClip destroy_sound;

    public int fuse_time;

    AudioSource audio_source;

    // Use this for initialization
    void Start()
    {
        Invoke("Explode", fuse_time);
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = shot_sound;
        audio_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 1000f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        audio_source.Stop();
        audio_source.PlayOneShot(destroy_sound, 1F);
        Explode();
    }

    void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, 0.25f);
    }




}
