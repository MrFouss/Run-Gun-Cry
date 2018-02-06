using UnityEngine;
using System.Collections;

public class MissileBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public AudioClip DestroySound;

    public int FuseTime;

    AudioSource AudioSource;

    // Use this for initialization
    void Start()
    {
        Invoke("Explode", FuseTime);
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = ShotSound;
        AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 1000f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.Stop();
        AudioSource.PlayOneShot(DestroySound, 1F);
        Explode();
    }

    void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, 0.25f);
    }




}
