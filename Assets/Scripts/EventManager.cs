using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// It is necessary to extend UnityEvent<...> classes because they are abstract

[System.Serializable]
public class CustomEvent1<T> : UnityEvent<T> {}

[System.Serializable]
public class CustomEvent2<T1, T2> : UnityEvent<T1, T2> { }

public enum EnemyType { Obstacle, Shooter, Charger, Coward };
public enum DestructionType { Shot, Collided };
public enum ShotType { Laser, Missile };
public enum DamageSourceType { CollidingObstacle, CollidingCharger, FallingIntoVoid, HitByEnemyLaser};

public class EventManager {

    // note : percentages are floats between 0 and 1
    
    // Letter typing

    // when a keyboard letter is typed <letter, successful>
    public static readonly UnityEvent<char, bool> onLetterTyped = new CustomEvent2<char, bool>();

    // when the current letter changes (for UI) <letter>
    public static readonly UnityEvent<char> onCurrentLetterChange = new CustomEvent1<char>();

    // when next letters list changes (for UI) <nextLetters>
    public static readonly UnityEvent<char[]> onNextLettersChange = new CustomEvent1<char[]>();

    // when past letters list changes (for UI) <pastLetters, succeful>
    public static readonly UnityEvent<char[], bool[]> onPastLettersChange = new CustomEvent2<char[], bool[]>();

    // combo

    // when the combo multiplier value changes <multiplier> (for UI)
    public static readonly UnityEvent<int> onComboMultiplierChange = new CustomEvent1<int>();

    // when the combo value changes <combo> (for UI)
    public static readonly UnityEvent<long> onComboChange = new CustomEvent1<long>();

    // when the remaining time before a time out changes <remaining percentage> (for UI)
    public static readonly UnityEvent<float> onComboRemainingTimeChange = new CustomEvent1<float>();

    // when a letter is successfully typed and whichever selected ressource should be reloaded <multiplier>
    public static readonly UnityEvent<long> requestReloadRessource = new CustomEvent1<long>();

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


    // stats

    // when these stats change <remaining percentage> (for UI)
    public static readonly UnityEvent<float> onShieldChange = new CustomEvent1<float>();
    public static readonly UnityEvent<float> onHealthChange = new CustomEvent1<float>();
    public static readonly UnityEvent<float> onEnergyChange = new CustomEvent1<float>();
    public static readonly UnityEvent<long> onScoreChange = new CustomEvent1<long>();

    // when selected filter change <filter color>
    public static readonly UnityEvent<FilterSelector.FilterColor> onFilterSelected = new CustomEvent1<FilterSelector.FilterColor>();

    public static readonly UnityEvent<Vector3> onCrosshairPositionChange = new CustomEvent1<Vector3>();
}
