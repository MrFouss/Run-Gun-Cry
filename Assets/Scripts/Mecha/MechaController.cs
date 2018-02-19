using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechaController : MonoBehaviour
{
    public enum ReloadType {ENERGY, SHIELD};

    public ReloadType ReloadedResource = ReloadType.ENERGY;
    public int BaseShieldReload = 1;
    public int BaseEnergyReload = 1;

    // resources
    public int MaxHealth = 100;
    public int MaxShield = 100;
    public int MaxEnergy = 70;
    public int EnergyConsumptionPerSecond = 1;
    public int HealthThreshold = 10;
    public int EnergyThreshold = 10;

    public AudioClip LowHealthSound;
    public AudioClip LowEnergySound;

    public Animation LowHealthAnimation;
    public Animation LowEnergyAnimation;

    private int _health;
    public int Health
    {
        set
        {
            _health = Mathf.Min(value, MaxHealth);
            EventManager.onHealthChange.Invoke((float)_health/MaxHealth);
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

    // heightRespawn after being positionned over a platform after falling into the void
    private int heightRespawn = 0;

    public Transform LastPlatformCoordinates;

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
        if (Health <= HealthThreshold && Shield == 0)
        {
            // TODO uncomment when files are added
            //audioSource.clip = LowHealthSound;
            //audioSource.Play();
            //LowHealthAnimation.Play();
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
        if (Energy <= EnergyThreshold)
        {
            // TODO uncomment when files are added
            //audioSource.clip = LowEnergySound;
            //audioSource.Play();
            //LowEnergyAnimation.Play();
        }
    }

    public bool CanConsumeEnergy(int consumption)
    {
        return Energy - consumption >= 0;
    }

    public void ReloadResource(int multiplier)
    {
        switch (ReloadedResource)
        {
            case ReloadType.ENERGY:
                Energy = Mathf.Min(MaxEnergy, Energy + multiplier * BaseEnergyReload);
                break;
            case ReloadType.SHIELD:
                Shield = Mathf.Min(MaxShield, Shield + multiplier * BaseShieldReload);
                break;
        }
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
                transform.position = new Vector3(LastPlatformCoordinates.position.x, LastPlatformCoordinates.position.y + heightRespawn, LastPlatformCoordinates.position.z);
                transform.eulerAngles = new Vector3(LastPlatformCoordinates.eulerAngles.x, LastPlatformCoordinates.eulerAngles.y, LastPlatformCoordinates.eulerAngles.z);
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

            case (Tags.PlatformTag):
                LastPlatformCoordinates = other.gameObject.transform;
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
