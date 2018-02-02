using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerCombo : MonoBehaviour {
    
    // max multiplier
    public long maxMultiplier = 4;


    // number of consecutive successful letters
    private int combo = 0;

    // the multiplier of the ressource reloaded
    // for exemple, if multiplier = 2, each letter typed reloads 2x the base reload
    private long multiplier = 1;

    void Awake()
    {
        EventManager.onLetterTyped.AddListener(OnLetterTyped);
    }

    void Start()
    {
        EventManager.onMultiplierChange.Invoke(multiplier);
        EventManager.onComboChange.Invoke(combo);
    }

    private void OnLetterTyped(char letter, bool successful)
    {
        // increment combo count
        if (successful)
        {
            combo++;
        } else
        {
            combo = 0;
        }

        // compute multiplier

        // the relation between combo and multiplier is as follows :
        // combo bounds -> multiplier
        // [2^0 ; 2^2 - 1] -> 1
        // [2^2 ; 2^4 - 1] -> 2
        // [2^4 ; 2^6 - 1] -> 3
        // ...
        // [2^n ; 2^(n+2) - 1] -> n/2 + 1 (n is even)

        // log(0) = - infinity, so make it at least log(1) = 0
        long closestPowerOf2 = (long)Mathf.Floor(Mathf.Log(Mathf.Max(combo, 1), 2));
        if (closestPowerOf2 % 2 == 1)
        {
            // the power of 2 is odd, make it even
            closestPowerOf2--;
        }
        // cap multiplier to maxMultiplier at most
        multiplier = (long)Mathf.Min(maxMultiplier, closestPowerOf2/2+1);

        EventManager.onMultiplierChange.Invoke(multiplier);
        EventManager.onComboChange.Invoke(combo);
    }
}
