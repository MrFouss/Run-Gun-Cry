using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersDisplay : MonoBehaviour {

    public Text currentLetterText;
    public Text previousLettersText;
    public Text nextLettersText;
    public Text comboText;
    public Text multiplierText;
    public ProgressBar timeOutBar;

    // color of the successful letters
    public Color successfulLettersColor = Color.green;

    // above this combo, the multiplier does not change
    // the color of the combo text will change depending on the combo value utile this value is reached
    public int comboTopValue = 64;

    // Use this for initialization
    void Awake () {
        EventManager.onCurrentLetterChange.AddListener(UpdateCurrentLetter);
        EventManager.onNextLettersChange.AddListener(UpdateNextLetters);
        EventManager.onPastLettersChange.AddListener(UpdatePastLetters);
        EventManager.onComboMultiplierChange.AddListener(UpdateComboMultiplier);
        EventManager.onComboChange.AddListener(UpdateCombo);
        EventManager.onComboRemainingTimeChange.AddListener(UpdateComboRemainingTime);
    }

    private void UpdateComboRemainingTime(float remainingPercentage)
    {
        timeOutBar.Progress = remainingPercentage;
        timeOutBar.gameObject.SetActive(remainingPercentage != 0);
        foreach (Renderer r in timeOutBar.gameObject.GetComponentsInChildren<Renderer>())
        {
            Debug.Log("hiding bar");
            r.enabled = remainingPercentage != 0;
        }
    }

    private void UpdateComboMultiplier(long combo)
    {
        multiplierText.text = "x" + combo;
    }

    private void UpdateCombo(long combo)
    {
        // combo might be greater than comboTopValue
        Color color = Color.HSVToRGB((float)0.9f * Mathf.Min(combo, comboTopValue) / comboTopValue, 1f, 0.8f);
        comboText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">Combo x" + combo + "</color>";
    }

    private void UpdateCurrentLetter(char newLetter)
    {
        currentLetterText.text = newLetter.ToString();
    }

    private void UpdateNextLetters(char[] nextLetters)
    {
        string tmp = "";

        for (int i = 0; i < nextLetters.Length; ++i)
        {
            tmp += nextLetters[i];
        }

        nextLettersText.text = tmp;
    }

    private void UpdatePastLetters(char[] pastLetters, bool[] success)
    {
        string tmp = "";

        for (int i = 0; i < pastLetters.Length; ++i)
        {
            if (success[i])
            {
                tmp += "<color=#" + ColorUtility.ToHtmlStringRGB(successfulLettersColor) + ">" + pastLetters[i] + "</color>";
            }
            else
            {
                tmp += pastLetters[i];
            }
        }

        previousLettersText.text = tmp;
    }

}
