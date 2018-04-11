using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechaController : MonoBehaviour
{
    public enum ReloadType {ENERGY, SHIELD};

    private ReloadType _reloadedResource = ReloadType.ENERGY;
    public ReloadType ReloadedResource
    {
        get
        {
            return _reloadedResource;
        }
        set
        {
            switch (value)
            {
                case ReloadType.ENERGY:
                    EventManager.Instance.OnEnergyReloadChange.Invoke(1);
                    EventManager.Instance.OnShieldReloadChange.Invoke(0);
                    break;
                case ReloadType.SHIELD:
                    EventManager.Instance.OnEnergyReloadChange.Invoke(0);
                    EventManager.Instance.OnShieldReloadChange.Invoke(1);
                    break;
            }

            _reloadedResource = value;
        }
    }


    public int BaseShieldReload = 3;
    public int BaseEnergyReload = 5;

    // resources
    public int MaxHealth = 100;
    public int MaxShield = 100;
    public int MaxEnergy = 100;
    public int EnergyConsumptionPerSecond;
    public int BaseConsumptionValue = 3;
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
            EventManager.Instance.OnHealthChange.Invoke((float)_health/MaxHealth);
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
            EventManager.Instance.OnShieldChange.Invoke((float)_shield/MaxHealth);
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
            EventManager.Instance.OnEnergyChange.Invoke((float)_energy/MaxEnergy);
        }
        get
        {
            return _energy;
        }
    }


    private int _distanceTravelled;
    public int DistanceTravelled
    {
        set
        {
            _distanceTravelled = value;
            EventManager.onDistanceTravelledChange.Invoke(_distanceTravelled);
        }
        get
        {
            return _distanceTravelled;
        }
    }

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

    // heightRespawn after being positionned over a platform after falling into the void
    public float RespawnHeight = 0.0f;

    private Vector3 lastPlatformPosition;

    //Difficulty curve
    private int previousScore = 0;
    public int StepToRaiseDifficulty = 1000;
    public float VoidDamageToIncreaseInPercentage = 1.0f;
    public float MissileConsumptionToIncreaseInPercentage = 1.0f;
    public float LaserConsumptionToIncreaseInPercentage = 1.0f;
    public float EnergyConsumptionToIncreaseInPercentage = 0.0f;

    // Use this for initialization
    void Start()
    {
        // set AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;

        Health = MaxHealth;
        Shield = MaxShield;
        Energy = MaxEnergy;

        ReloadedResource = ReloadType.ENERGY;

        // consume energy every second
        InvokeRepeating("ConsumeEnergyPassive", 1, 1);

        // update distance travelled
        InvokeRepeating("UpdateDistanceTravelled", 0.1f, 0.1f);

        // inform the scoring of the shield amount every second
        InvokeRepeating("SendShieldData", 1, 1);
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
        EventManager.Instance.OnHealthLow.Invoke(Health <= HealthThreshold && Shield == 0);
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
        EventManager.Instance.OnEnergyLow.Invoke(Energy <= EnergyThreshold);
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
                // event for the scoring script
                EventManager.onEngineerReload.Invoke(ReloadType.ENERGY, multiplier * BaseEnergyReload);
                break;
            case ReloadType.SHIELD:
                Shield = Mathf.Min(MaxShield, Shield + multiplier * BaseShieldReload);
                // event for the scoring script
                EventManager.onEngineerReload.Invoke(ReloadType.SHIELD, multiplier * BaseEnergyReload);
                break;
        }
    }
    
    private void UpdateDistanceTravelled()
    {
        DistanceTravelled = (int)Mathf.Floor(gameObject.transform.position.z);
    }
    
    private void SendShieldData()
    {
        EventManager.onShieldDataSending.Invoke(Shield);
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case Tags.EnemyChargerTag:
                // TODO uncomment when these files are added
                // audioSource.clip = EnemyCollisionDamageSound;
                // audioSource.Play();
                // let the scoring script know about the damage taken
                EventManager.onDamageTaken.Invoke(DamageSourceType.CollidingCharger, VoidDamage);
                GetComponent<CameraController>().IncreaseMovingFactor();
                TakeDamage(other.gameObject.GetComponent<EnemyController>().Damage);
                break;

            case Tags.VoidTag:
                // TODO uncomment when these files are added
                // audioSource.clip = VoidDamageSound;
                // audioSource.Play();

                GetComponent<CameraController>().IncreaseMovingFactor();
                TakeDamage(VoidDamage);
                transform.position = new Vector3(lastPlatformPosition.x, lastPlatformPosition.y, lastPlatformPosition.z);
                transform.eulerAngles = Quaternion.LookRotation(Vector3.forward, -1.0f * transform.position).eulerAngles;
                transform.Translate(Vector3.up, Space.Self);
                break;

            case Tags.ObstacleWallTag:
                // TODO uncomment when these files are added
                // audioSource.clip = WallObstacleDamageSound;
                // audioSource.Play();
                GetComponent<CameraController>().IncreaseMovingFactor();
                // let the scoring script know about the damage taken
                int obstacleWallDamage = other.gameObject.GetComponent<ObstacleWallController>().Damage;
                EventManager.onDamageTaken.Invoke(DamageSourceType.CollidingObstacle, obstacleWallDamage);
                TakeDamage(obstacleWallDamage);
                break;

            case (Tags.PlatformTag):
                Vector3 oldPosition = other.gameObject.transform.position;
                lastPlatformPosition = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);
                break;

            case Tags.EnemyLaserTag:
                // TODO uncomment when these files are added
                // audioSource.clip = LaserDamageSound;
                // audioSource.Play();
                GetComponent<CameraController>().IncreaseMovingFactor();
                // let the scoring script know about the damage taken
                int enemyLaserDamage = other.gameObject.GetComponentInParent<ProjectileBehavior>().Damage;
                EventManager.onDamageTaken.Invoke(DamageSourceType.HitByEnemyLaser, enemyLaserDamage);
                TakeDamage(enemyLaserDamage);
                break;

            default:
                break;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void OnSpeedFirePowerBalanceChange(float balanceValue)
    {
        //EnergyConsumptionPerSecond = (int) ((balanceValue + 2.0f) * BaseConsumptionValue);
    }

    public void Update()
    {
        int score = gameObject.GetComponent<Scoring>().Score;

        if (score - previousScore >= StepToRaiseDifficulty)
        {
            previousScore = score;
            VoidDamage = (int) Mathf.Ceil(VoidDamage * (1.0f + VoidDamageToIncreaseInPercentage / 100.0f));
            EnergyConsumptionPerSecond = (int) Mathf.Ceil(EnergyConsumptionPerSecond * (1.0f + EnergyConsumptionToIncreaseInPercentage / 100.0f));
            gameObject.GetComponent<CannonBehavior>().LaserEnergyConsumption = (int) Mathf.Ceil(gameObject.GetComponent<CannonBehavior>().LaserEnergyConsumption * (1.0f + LaserConsumptionToIncreaseInPercentage / 100.0f));
            gameObject.GetComponent<CannonBehavior>().MissileEnergyConsumption = (int) Mathf.Ceil(gameObject.GetComponent<CannonBehavior>().MissileEnergyConsumption * (1.0f + MissileConsumptionToIncreaseInPercentage / 100.0f));
        }
       
    }

}
