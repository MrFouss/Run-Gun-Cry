using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// It is necessary to extend UnityEvent<...> classes because they are abstract

[System.Serializable]
public class CustomEvent1<T> : UnityEvent<T> {}

[System.Serializable]
public class CustomEventString : UnityEvent<string> { }

[System.Serializable]
public class CustomEventFloat : UnityEvent<float> { }

[System.Serializable]
public class CustomEventFilterColor : UnityEvent<FilterColor> { }

[System.Serializable]
public class CustomEventVector2 : UnityEvent<Vector2> { }

[System.Serializable]
public class CustomEvent2<T1, T2> : UnityEvent<T1, T2> { }

public enum EnemyType { Obstacle, Shooter, Charger, Coward };
public enum DestructionType { Shot, Collided };
public enum ShotType { Laser, Missile };
public enum DamageSourceType { CollidingObstacle, CollidingCharger, FallingIntoVoid, HitByEnemyLaser};

public class EventManager : MonoBehaviour {

    // singleton instance

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnDestroy()
    {
        if (this == _instance)
        {
            _instance = null;
        }
    }

    // note : percentages are floats between 0 and 1
    
    // when the current letter changes (for UI) <letter>
    public CustomEventString OnCurrentLetterChange;

    // when next letters list changes (for UI) <nextLetters>
    public CustomEventString OnNextLettersChange;

    // when past letters list changes (for UI) <pastLetters, succeful>
    public CustomEventString OnPastLettersChange;

    // when the remaining time before a time out changes <remaining percentage> (for UI)
    public CustomEventFloat OnComboRemainingTimeChange;

    // when the combo multiplier value changes <multiplier> (for UI)
    public CustomEventString OnComboMultiplierChange;

    // when the combo value changes <combo> (for UI)
    public CustomEventString onComboChange;

    // when these stats change <remaining percentage> (for UI)
    public CustomEventFloat OnShieldChange;
    public CustomEventFloat OnHealthChange;
    public CustomEventFloat OnEnergyChange;
    public CustomEventString OnScoreChange;

    // when selected filter change <filter color> (for UI)
    public CustomEventFilterColor OnFilterSelected;
    
    // when the target crosshair position should change <position in screen space> (for UI)
    public CustomEventVector2 OnCrosshairPositionChange;



    // TODO remove static fields

    // scoring

    // when an enemy is destroyed by the player <EnemyType, DestructionType>
    public static readonly UnityEvent<EnemyType, DestructionType> onEnemyDestruction = new CustomEvent2<EnemyType, DestructionType>();
    // when the gunner shoots <ShotType>
    public static readonly UnityEvent<ShotType> onGunnerShot = new CustomEvent1<ShotType>();
    // when a projectile hits <ShotType>
    public static readonly UnityEvent<ShotType> onShotHitting = new CustomEvent1<ShotType>();
    // when the mecha takes damage <damageSource, amount>
    public static readonly UnityEvent<DamageSourceType, int> onDamageTaken = new CustomEvent2<DamageSourceType, int>();
    // when the mecha moves <distanceTravelledSoFar>
    public static readonly UnityEvent<int> onDistanceTravelledChange = new CustomEvent1<int>();
    // when the gunner consumes energy <amount>
    public static readonly UnityEvent<int> onGunnerConsumesEnergy = new CustomEvent1<int>();
    // when the engineer reloads shield or energy <ReloadType, amount>
    public static readonly UnityEvent<MechaController.ReloadType, int> onEngineerReload = new CustomEvent2<MechaController.ReloadType, int>();
    // every second, shield data must be sent to calculate average shield
    public static readonly UnityEvent<int> onShieldDataSending = new CustomEvent1<int>();
    
}
