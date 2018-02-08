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

    // damage properties
    public int LaserDamage = 5;
    public int VoidDamage = 30;
    public int EnemyCollisionDamage = 100;
    public int WallObstacleDamage = 50;

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

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        Shield -= damage;
        if (Shield < 0)
        {
            Health -= Shield;
            Shield = 0;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case (Tags.EnemyLaserTag):
                // TODO uncomment when these files are added
                // audioSource.clip = LaserDamageSound;
                // audioSource.Play();
                // LaserDamageAnimation.Play();

                TakeDamage(LaserDamage);
                break;

            case (Tags.VoidTag):
                // TODO uncomment when these files are added
                // audioSource.clip = VoidDamageSound;
                // audioSource.Play();
                // VoidDamageAnimation.Play();

                TakeDamage(VoidDamage);
                break;

            case (Tags.EnemyChargerTag):
                // TODO uncomment when these files are added
                // audioSource.clip = EnemyCollisionDamageSound;
                // audioSource.Play();
                // EnemyCollisionDamageAnimation.Play();

                TakeDamage(EnemyCollisionDamage);
                break;

            case (Tags.ObstacleWallTag):
                // TODO uncomment when these files are added
                // audioSource.clip = WallObstacleDamageSound;
                // audioSource.Play();
                // WallObstacleDamageAnimation.Play();

                TakeDamage(WallObstacleDamage);
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
