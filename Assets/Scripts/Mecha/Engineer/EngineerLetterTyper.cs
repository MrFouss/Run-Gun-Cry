using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EngineerLetterTyper : MonoBehaviour
{

    // max multiplier
    public int MaxMultiplier = 4;

    // if the player doesn't type a successful letter before timeout, the combo is reset
    public float TimeOutSec = 1;

    private float _remainingTimeSec;
    private float RemainingTimeSec
    {
        set
        {
            _remainingTimeSec = value;
            EventManager.Instance.OnComboRemainingTimeChange.Invoke(Mathf.Max(0, _remainingTimeSec / TimeOutSec));
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

            EventManager.Instance.onComboChange.Invoke(_combo.ToString());

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
            Multiplier = (int)Mathf.Min(MaxMultiplier, closestPowerOf2 / 2 + 1);
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
            EventManager.Instance.OnComboMultiplierChange.Invoke("x" + _multiplier);
        }
    }

    private MechaController mechaController;
    private Scoring scoring;

    public int NextLettersNumber = 4;
    public int PastLettersNumber = 4;

    private char currLetter;
    private List<char> nextLetters = new List<char>();
    private List<char> pastLetters = new List<char>();
    private List<bool> pastLettersSuccess = new List<bool>();

    // arrow characters
    private const char _leftArrow = '←';
    private const char _rightArrow = '→';
    private const char _downArrow = '↓';
    private const char _upArrow = '↑';

    // Use this for initialization
    void Start()
    {
        mechaController = GetComponent<MechaController>();
        scoring = GetComponent<Scoring>();

        // init variables
        RemainingTimeSec = 0;
        Combo = 0;

        // randomly init current and next letters

        currLetter = GenerateRandomCharacter();
        EventManager.Instance.OnCurrentLetterChange.Invoke(currLetter.ToString());

        for (int i = 0; i < NextLettersNumber; ++i)
        {
            nextLetters.Add(GenerateRandomCharacter());
        }
        EventManager.Instance.OnNextLettersChange.Invoke(new string(nextLetters.ToArray()));

        EventManager.Instance.OnPastLettersChange.Invoke("");
    }

    // Update is called once per frame
    void Update()
    {
        // check if toggling reloaded resource
        if (Input.GetButtonDown("ToggleReloadedResource"))
        {
            switch (mechaController.ReloadedResource)
            {
                case MechaController.ReloadType.ENERGY:
                    mechaController.ReloadedResource = MechaController.ReloadType.SHIELD;
                    break;
                case MechaController.ReloadType.SHIELD:
                    mechaController.ReloadedResource = MechaController.ReloadType.ENERGY;
                    break;
            }
        }

        // arrow typed ?
        char key = '0';
        if (Input.GetButtonDown("UpArrow"))
        {
            key = _upArrow;
        }
        else if (Input.GetButtonDown("DownArrow"))
        {
            key = _downArrow;
        }
        else if(Input.GetButtonDown("LeftArrow"))
        {
            key = _leftArrow;
        }
        else if(Input.GetButtonDown("RightArrow"))
        {
            key = _rightArrow;
        }

        // arrow typed
        if (key != '0')
        {
            if (key == currLetter)
            {
                // correct arrow entered
                pastLettersSuccess.Add(true);
                OnLetterTyped(key, true);
            }
            else
            {
                // wrong arrow entered
                pastLettersSuccess.Add(false);
                OnLetterTyped(key, false);
            }
        
            // update past letters
            pastLetters.Add(currLetter);
            if (pastLetters.Count > PastLettersNumber)
            {
                pastLetters.RemoveAt(0);
                pastLettersSuccess.RemoveAt(0);
            }
            EventManager.Instance.OnPastLettersChange.Invoke(new string(pastLetters.ToArray()));

            // update curr letter
            currLetter = nextLetters[0];
            EventManager.Instance.OnCurrentLetterChange.Invoke(currLetter.ToString());

            // update next letters
            nextLetters.RemoveAt(0);
            nextLetters.Add(GenerateRandomCharacter());
            EventManager.Instance.OnNextLettersChange.Invoke(new string(nextLetters.ToArray()));
        }

        
        
        // check timout
        RemainingTimeSec -= Time.deltaTime;
        if (RemainingTimeSec <= 0)
        {
            Combo = 0;
        }
    }

    private void OnLetterTyped(char letter, bool successful)
    {
        scoring.OnLetterTyped(successful);
        // increment combo count
        if (successful)
        {
            mechaController.ReloadResource(Multiplier);
            Combo++;
            // reset remainingTime
            RemainingTimeSec = TimeOutSec;
        }
        else
        {
            Combo = 0;
            RemainingTimeSec = 0;
        }
    }

    private char GenerateRandomCharacter() {
        int i = Random.Range(0, 4);
        switch (i) {
            case 0:
                return _leftArrow;
            case 1:
                return _rightArrow;
            case 2:
                return _downArrow;
            case 3:
                return _upArrow;
            default:
                throw new KeyNotFoundException();
        }
    }
}
