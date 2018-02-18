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
    public static readonly UnityEvent<long> onComboMultiplierChange = new CustomEvent1<long>();

    // when the combo value changes <combo> (for UI)
    public static readonly UnityEvent<long> onComboChange = new CustomEvent1<long>();

    // when the remaining time before a time out changes <remaining percentage> (for UI)
    public static readonly UnityEvent<float> onComboRemainingTimeChange = new CustomEvent1<float>();

    // when a letter is successfully typed and whichever selected ressource should be reloaded <multiplier>
    public static readonly UnityEvent<long> requestReloadRessource = new CustomEvent1<long>();

    // scoring

    // when an enemy is destroyed by the player
    public static readonly UnityEvent<EnemyType, DestructionType> onEnemyDestruction = new CustomEvent2<EnemyType, DestructionType>();

    // when the gunner shoots
    public static readonly UnityEvent<ShotType> onGunnerShot = new CustomEvent1<ShotType>();

    // when a projectile hits
    public static readonly UnityEvent<ShotType> onShotHitting = new CustomEvent1<ShotType>();

    // when the mecha takes damage
    public static readonly UnityEvent<DamageSourceType, int> onDamageTaken = new CustomEvent2<DamageSourceType, int>();

}
