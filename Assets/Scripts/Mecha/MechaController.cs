using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechaController : MonoBehaviour
{
    // resources
    public int Health = 100;
    public int Shield = 100;
    public int Energy = 75;

    // sound
    private AudioSource audioSource;

    // TODO Delete if Damages function works
    public int VoidDamage = 30;

    public AudioClip LaserDamageSound;
    public AudioClip VoidDamageSound;
    public AudioClip EnemyCollisionDamageSound;
    public AudioClip WallObstacleDamageSound;

    // animation
    public Animation LaserDamageAnimation;
    public Animation VoidDamageAnimation;
    public Animation EnemyCollisionDamageAnimation;
    public Animation WallObstacleDamageAnimation;

    // Use this for initialization
    void Start()
    {
        // set AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;
    }

    public void TakeDamage(int damage)
    {
        Shield -= damage;

        if (Shield < 0)
        {
            Health += Shield;
            Shield = 0;

            if (Health <= 0) {
                GameOver();
            }
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            
            case Tags.EnemyChargerTag:
                // TODO uncomment when these files are added
                // audioSource.clip = EnemyCollisionDamageSound;
                // audioSource.Play();
                // EnemyCollisionDamageAnimation.Play();

                TakeDamage(other.gameObject.GetComponent<EnemyController>().Damage);
                break;

            case Tags.ObstacleWallTag:
                // TODO uncomment when these files are added
                // audioSource.clip = WallObstacleDamageSound;
                // audioSource.Play();
                // WallObstacleDamageAnimation.Play();

                TakeDamage(other.gameObject.GetComponent<ObstacleWallController>().Damage);
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case Tags.EnemyLaserTag:
                // TODO uncomment when these files are added
                // audioSource.clip = LaserDamageSound;
                // audioSource.Play();
                // LaserDamageAnimation.Play();
                TakeDamage(other.gameObject.GetComponent<ProjectileBehavior>().Damage);
                break;

            case Tags.VoidTag:
                // TODO uncomment when these files are added
                // audioSource.clip = VoidDamageSound;
                // audioSource.Play();
                // VoidDamageAnimation.Play();
                TakeDamage(VoidDamage);
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
