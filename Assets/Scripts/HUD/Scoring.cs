﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {

    private int _score;
    public int Score
    {
        set
        {
            TeamScore = _score;
            _score = Mathf.Max(0, value);
            EventManager.Instance.OnScoreChange.Invoke(_score.ToString());
        }
        get
        {
            return _score;
        }
    }
    private int scoreDistanceTravelled = 0;
    private int scoreOthers = 0;

    private int collidedObstacles = 0;
    private int collidedChargers = 0;
    private int collidedShooters = 0;
    private int collidedCowards = 0;

    private int destroyedObstacles = 0;
    private int destroyedChargers = 0;
    private int destroyedShooters = 0;
    private int destroyedCowards = 0;

    private float laserShots = 0;
    private float missilesShot = 0;
    private float laserHits = 0;
    private float missilesHit = 0;

    private float lettersCorrect = 0;
    private float lettersFailed = 0;

    private int damageTakenFromChargers = 0;
    private int damageTakenFromLasers = 0;
    private int damageTakenFromVoid = 0;
    private int damageTakenFromObstacles = 0;

    private int shieldReloaded = 0;
    private float energyReloaded = 0;

    private float energyConsumedByGunner = 0;

    private List<int> shieldAmounts = new List<int>();


    // Static variables for GameOver screen
    public static int TeamScore;
    public static int PilotDamageTaken;
    public static int PilotDistanceTravelled;
    public static float GunnerAccuracy;
    public static int GunnerDamageTaken;
    public static float GunnerEnergyConsumption;
    public static float EngineerAccuracy;
    public static int EngineerShieldGenerated;
    public static float EngineerEnergyGenerated;
    public static int EngineerTimeWithoutEnergy;
    public static float EngineerAverageEnergy;


    // Use this for initialization
    void Awake () {
        EventManager.onEnemyDestruction.AddListener(OnEnemyDestruction);
        EventManager.onGunnerShot.AddListener(OnGunnerShot);
        EventManager.onShotHitting.AddListener(OnShotHitting);
        EventManager.onDamageTaken.AddListener(OnDamageTaken);
        EventManager.onDistanceTravelledChange.AddListener(OnDistanceTravelledChange);
        EventManager.onEngineerReload.AddListener(OnEngineerReload);
        EventManager.onGunnerConsumesEnergy.AddListener(OnGunnerConsumesEnergy);
        EventManager.onShieldDataSending.AddListener(OnShieldDataSending);
	}
	
	// Update is called once per frame
	void Update () {
        Score = scoreDistanceTravelled + scoreOthers;
	}

    private void OnDestroy()
    {
        PilotDamageTaken = GetDamageFromPilotMistakes();
        PilotDistanceTravelled = GetDistanceTravelled();
        GunnerAccuracy = GetGunnerAccuracy();
        GunnerDamageTaken = GetDamageFromGunnerMistakes();
        GunnerEnergyConsumption = GetGunnerEnergyConsumption();
        EngineerAccuracy = GetEngineerAccuracy();
        EngineerShieldGenerated = GetShieldGeneratedByEngineer();
        EngineerEnergyGenerated = GetEnergyGeneratedByEngineer();
        EngineerTimeWithoutEnergy = GetTimeSpentWithoutEnergy();
        EngineerAverageEnergy = GetAverageEnergy();
        

    }

    private void OnEnemyDestruction(EnemyType enemyType, DestructionType destructionType)
    {
        if (destructionType == DestructionType.Shot)
        {
            switch (enemyType)
            {
                case (EnemyType.Obstacle):
                    scoreOthers += 10;
                    destroyedObstacles++;
                    break;
                case (EnemyType.Shooter):
                    scoreOthers += 30;
                    destroyedShooters++;
                    break;
                case (EnemyType.Charger):
                    scoreOthers += 50;
                    destroyedChargers++;
                    break;
                case (EnemyType.Coward):
                    scoreOthers += 70;
                    destroyedCowards++;
                    break;
                default:
                    break;
            }
        } else if (destructionType == DestructionType.Collided)
        {
            switch (enemyType)
            {
                case (EnemyType.Obstacle):
                    collidedObstacles ++;
                    break;
                case (EnemyType.Shooter):
                    collidedShooters ++;
                    break;
                case (EnemyType.Charger):
                    collidedChargers ++;
                    break;
                case (EnemyType.Coward):
                    collidedCowards ++;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnGunnerShot(ShotType shotType)
    {
        if (shotType == ShotType.Laser)
        {
            laserShots++;
        }
		else
        {
            missilesShot++;
        }
    }

    private void OnShotHitting(ShotType shotType)
    {
        if (shotType == ShotType.Laser)
        {
            laserHits++;
        }
		else if (shotType == ShotType.Missile)
        {
            missilesHit++;
        }
    }

    public void OnLetterTyped(bool success)
    {
        if (success)
        {
            lettersCorrect++;
        } else
        {
            lettersFailed++;
        }
    }

    private void OnDamageTaken(DamageSourceType damageSourceType, int damageTaken)
    {
        switch (damageSourceType)
        {
            case (DamageSourceType.CollidingCharger):
                damageTakenFromChargers += damageTaken;
                break;
            case (DamageSourceType.CollidingObstacle):
                damageTakenFromObstacles += damageTaken;
                break;
            case (DamageSourceType.FallingIntoVoid):
                damageTakenFromVoid += damageTaken;
                break;
            case (DamageSourceType.HitByEnemyLaser):
                damageTakenFromLasers += damageTaken;
                break;
            default:
                break;
        }
        
    }

    private void OnDistanceTravelledChange(int distance)
    {
        scoreDistanceTravelled = Mathf.Max(distance, scoreDistanceTravelled);
    }

    private void OnEngineerReload(MechaController.ReloadType reloadType, int amount)
    {
        if (reloadType == MechaController.ReloadType.ENERGY)
        {
            energyReloaded += amount;
        } else
        {
            shieldReloaded += amount;
        }
    }

    private void OnGunnerConsumesEnergy(int consumption)
    {
        energyConsumedByGunner += consumption;
    }

    private void OnShieldDataSending(int amount)
    {
        shieldAmounts.Add(amount);
    }

    // Methods to call to get statistics for the GameOver scene
    // For the pilot :
    public int GetDamageFromPilotMistakes()
    {
        return (damageTakenFromVoid + damageTakenFromObstacles);
    }

    public int GetDistanceTravelled()
    {
        return scoreDistanceTravelled;
    }

    // For the gunner :
    public float GetGunnerAccuracy()
    {
        return ((laserHits + missilesHit) / Mathf.Max(1.0f, laserShots + missilesShot)) * 100.0f;
    }

    public int GetDamageFromGunnerMistakes()
    {
        return (damageTakenFromChargers + damageTakenFromLasers);
    }

    public float GetGunnerEnergyConsumption()
    {
        return (energyConsumedByGunner);
    }

    // For the engineer
    public float GetEngineerAccuracy()
    {
		return (lettersCorrect / Mathf.Max(1.0f, lettersCorrect + lettersFailed)) * 100.0f;
    }

    public int GetShieldGeneratedByEngineer()
    {
        return shieldReloaded;
    }

    public float GetEnergyGeneratedByEngineer()
    {
        return energyReloaded;
    }

    public int GetTimeSpentWithoutEnergy()
    {
        int secondsSpentWithoutEnergy = 0;
        for (int i=0; i<shieldAmounts.Count; i++)
        {
            if(shieldAmounts[i] == 0)
            {
                secondsSpentWithoutEnergy++;
            }
        }
        return secondsSpentWithoutEnergy;
    }

    public float GetAverageEnergy()
    {
        float average = 0f;
        for (int i = 0; i < shieldAmounts.Count; i++)
        {
            average += (float) shieldAmounts[i];
        }
        average /= Mathf.Max(1, shieldAmounts.Count);
        return average;
    }



    //TODO: add these lines of code in the destruction methods of enemies by 
    //if ((otherObject.tag == Tags.MechaLaserTag) || (otherObject.tag == Tags.MechaMissileTag))
    //        {
    //            EventManager.onEnemyDestruction.Invoke(EnemyType.ENEMYTYPE, DestructionType.Shot);
    //        }

    //TODO: add this line of code when an enemy is destroyed by colliding into the mecha
    //EventManager.onEnemyDestruction.Invoke(EnemyType.ENEMYTYPE, DestructionType.Collided);

}
