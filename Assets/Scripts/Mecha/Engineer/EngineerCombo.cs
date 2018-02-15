using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerCombo : MonoBehaviour {
    
    // max multiplier
    public int maxMultiplier = 4;

    // if the player doesn't type a successful letter before timeout, the combo is reset
    public float timeOutSec = 1;

    private float _remainingTimeSec;
    private float RemainingTimeSec
    {
        set
        {
            _remainingTimeSec = value;
            EventManager.onComboRemainingTimeChange.Invoke(Mathf.Max(0, _remainingTimeSec/timeOutSec));
        }
        get
        {
            return _remainingTimeSec;
        }
    }

    // number of consecutive successful letters
    private int _combo;
    private int Combo
    {
        get
        {
            return _combo;
        }
        set
        {
            _combo = value;

            EventManager.onComboChange.Invoke(_combo);

            // compute multiplier

            // the relation between combo and multiplier is as follows :
            // combo bounds -> multiplier
            // [2^0 ; 2^2 - 1] -> 1
            // [2^2 ; 2^4 - 1] -> 2
            // [2^4 ; 2^6 - 1] -> 3
            // ...
            // [2^n ; 2^(n+2) - 1] -> n/2 + 1 (n is even)

            // log(0) = - infinity, so make it at least log(1) = 0
            long closestPowerOf2 = (long)Mathf.Floor(Mathf.Log(Mathf.Max(_combo, 1), 2));
            if (closestPowerOf2 % 2 == 1)
            {
                // the power of 2 is odd, make it even
                closestPowerOf2--;
            }
            // cap multiplier to maxMultiplier at most
            Multiplier = (int)Mathf.Min(maxMultiplier, closestPowerOf2 / 2 + 1);
        }
    }

    // the multiplier of the ressource reloaded
    // it is always infered from combo (thus nerver directly affected in functions)
    // for exemple, if multiplier = 2, each letter typed reloads 2x the base reload
    private int _multiplier;
    private int Multiplier
    {
        get
        {
            return _multiplier;
        }
        set
        {
            _multiplier = value;
            EventManager.onComboMultiplierChange.Invoke(_multiplier);
        }
    }

    private MechaController mechaController;

    void Awake()
    {
        mechaController = GetComponent<MechaController>();
        EventManager.onLetterTyped.AddListener(OnLetterTyped);
    }

    void Start()
    {
        // init variables
        RemainingTimeSec = 0;
        Combo = 0;
    }

    private void Update()
    {
        // check timout
        RemainingTimeSec -= Time.deltaTime;
        if (RemainingTimeSec <= 0)
        {
            Combo = 0;
        }
    }

    private void OnLetterTyped(char letter, bool successful)
    {
        // increment combo count
        if (successful)
        {
            mechaController.ReloadResource(Multiplier);
            Combo++;
            // reset remainingTime
            RemainingTimeSec = timeOutSec;
        } else
        {
            Combo = 0;
            RemainingTimeSec = 0;
        }
    }
}
