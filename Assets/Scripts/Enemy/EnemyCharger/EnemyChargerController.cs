using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyChargerController : MonoBehaviour
{

    public float MaxSpeed = 50f;
    public float Acceleration = 100f;

    public int Damage = 25;

    public float DetectionRange = 20f;

    public int Health = 100;
    private bool startedCharge = false;

    private bool isInRange = false;
    private bool foundMecha = false;

    private GameObject mecha;
    private Rigidbody rb;

    public ParticleSystem Trail;

    public GameObject ExplosionParticleSystem;

    public AudioClip ExplosionClip;
    public AudioClip ChargeClip;

    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mecha = GameObject.FindGameObjectWithTag(Tags.MechaBodyTag);
        foundMecha = (mecha != null);
    }

    private void Update()
    {
        if(Health <= 0)
        {
            Explode();
        }
        else
        {
            if (foundMecha)
            {
                if (!isInRange)
                {
                    if (Vector3.Distance(transform.position, mecha.transform.position) <= DetectionRange)
                    {
                        isInRange = true;
                    }
                }
            }
            else
            {
                mecha = GameObject.FindGameObjectWithTag(Tags.MechaBodyTag);
                foundMecha = (mecha != null);
            }
        }
    }

    private void FixedUpdate()
    {
        if(isInRange)
        {
            if (!startedCharge)
            {
                //TODO : Uncomment when charge clip is available
                //audioSource.loop = true;
                //audioSource.clip = ChargeClip;
                //audioSource.Play();
                ParticleSystem trail = Instantiate(Trail, transform.position, Quaternion.Euler(180f, 0f, 0f));
                trail.transform.parent = transform;
                startedCharge = true;
            }
            rb.transform.LookAt(mecha.transform.position + mecha.GetComponent<Rigidbody>().velocity * Time.fixedDeltaTime);
            rb.velocity = transform.forward * (rb.velocity.magnitude + Acceleration * Time.fixedDeltaTime);
            if(rb.velocity.magnitude > MaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * MaxSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Tags.MechaBodyTag))
        {
            Explode();
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void Explode()
    {
        audioSource.Stop();

        AudioSource.PlayClipAtPoint(ExplosionClip, transform.position, 1f);
        Destroy(Instantiate(ExplosionParticleSystem, transform.position, Quaternion.Euler(0f, 0f, 0f)), 1f);
        Destroy(gameObject);
    }
}
