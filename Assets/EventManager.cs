using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomEvent1<T> : UnityEvent<T> {}

[System.Serializable]
public class CustomEvent2<T1, T2> : UnityEvent<T1, T2> { }

public class EngineerEventManager {

    public static readonly UnityEvent<char> onCurrentLetterChange = new CustomEvent1<char>();
    public static readonly UnityEvent<char[]> onNextLettersChange = new CustomEvent1<char[]>();
    public static readonly UnityEvent<char[], bool[]> onPastLettersChange = new CustomEvent2<char[], bool[]>();
}
