using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerCombo : MonoBehaviour {
    
    // max combo
    public long maxCombo = 256;

    // combo x2 every n successful letters
    public int x2ComboStep = 5;

    private int consecutiveSuccessfulLetters = 0;
    private long comboMultiplier = 1;

    void Awake()
    {
        EventManager.onLetterTyped.AddListener(OnLetterTyped);
    }

    void Start()
    {
        EventManager.onComboMultiplierChange.Invoke(comboMultiplier, maxCombo);
    }

    private void OnLetterTyped(char letter, bool successful)
    {
        if (successful)
        {
            consecutiveSuccessfulLetters++;
        } else
        {
            consecutiveSuccessfulLetters = 0;
        }

        comboMultiplier = (long)Mathf.Min(maxCombo, Mathf.Pow(2.0f, consecutiveSuccessfulLetters / x2ComboStep));
        EventManager.onComboMultiplierChange.Invoke(comboMultiplier, maxCombo);
    }
}
