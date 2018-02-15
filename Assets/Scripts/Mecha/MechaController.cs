using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechaController : MonoBehaviour
{
    // resources
    public int MaxHealth = 100;
    public int MaxShield = 100;
    public int MaxEnergy = 70;
    public int EnergyConsumptionPerSecond = 1;

    private int _health;
    public int Health
    {
        set
        {
            _health = Mathf.Min(value, MaxHealth);
            EventManager.onLifeChange.Invoke((float)_health/MaxHealth);
        }
        get
        {
            return _health;
        }
    }

    private int _shield;
    public int Shield
    {
        set
        {
            _shield = Mathf.Min(value, MaxShield);
            EventManager.onShieldChange.Invoke((float)_shield/MaxHealth);
        }
        get
        {
            return _shield;
        }
    }

    private int _energy;
    public int Energy
    {
        set
        {
            _energy = Mathf.Min(value, MaxEnergy);
            EventManager.onEnergyChange.Invoke((float)_energy/MaxEnergy);
        }
        get
        {
            return _energy;
        }
    }

    private int _score;
    public int Score
    {
        set
        {
            _score = Mathf.Max(0, value);
            EventManager.onScoreChange.Invoke(_score);
        }
        get
        {
            return _score;
        }
    }

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

        Health = MaxHealth;
        Shield = MaxShield;
        Energy = MaxEnergy;
        Score = 0;

        // consume energy every second
        InvokeRepeating("ConsumeEnergyPassive", 1, 1);
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

    // Passive energy consumption (every second)
    private void ConsumeEnergyPassive()
    {
        ConsumeEnergy(EnergyConsumptionPerSecond);
    }
    
    public void ConsumeEnergy(int consumption)
    {
        Energy = Mathf.Max(0, Energy - consumption);
    }
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case Tags.EnemyLaserTag:
                // TODO uncomment when these files are added
                // audioSource.clip = LaserDamageSound;
                // audioSource.Play();
                // LaserDamageAnimation.Play();
                TakeDamage(LaserDamage);
                break;

            case Tags.VoidTag:
                // TODO uncomment when these files are added
                // audioSource.clip = VoidDamageSound;
                // audioSource.Play();
                // VoidDamageAnimation.Play();
                TakeDamage(VoidDamage);
                break;

            case Tags.EnemyChargerTag:
                // TODO uncomment when these files are added
                // audioSource.clip = EnemyCollisionDamageSound;
                // audioSource.Play();
                // EnemyCollisionDamageAnimation.Play();
                TakeDamage(EnemyCollisionDamage);
                break;

            case Tags.ObstacleWallTag:
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
