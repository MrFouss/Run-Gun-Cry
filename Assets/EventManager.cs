using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// It is necessary to extend UnityEvent<...> classes because they are abstract

[System.Serializable]
public class CustomEvent1<T> : UnityEvent<T> {}

[System.Serializable]
public class CustomEvent2<T1, T2> : UnityEvent<T1, T2> { }

public class EventManager {

    /// Engineer letter typing events

    // when a keyboard letter is typed <letter, successful>
    public static readonly UnityEvent<char, bool> onLetterTyped = new CustomEvent2<char, bool>();

    // when the combo multiplier value changes <combo, maxCombo>
    public static readonly UnityEvent<long, long> onComboMultiplierChange = new CustomEvent2<long, long>();

    // when the current letter changes (for UI) <letter>
    public static readonly UnityEvent<char> onCurrentLetterChange = new CustomEvent1<char>();

    // when next letters list changes (for UI) <nextLetters>
    public static readonly UnityEvent<char[]> onNextLettersChange = new CustomEvent1<char[]>();

    // when past letters list changes (for UI) <pastLetters, succeful>
    public static readonly UnityEvent<char[], bool[]> onPastLettersChange = new CustomEvent2<char[], bool[]>();
}
