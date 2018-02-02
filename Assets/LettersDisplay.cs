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

    public Color successColor = Color.green;

    // the color of the combo text will change depending on the combo value utile this value is reached
    public int comboTopValue = 64;

    // Use this for initialization
    void Awake () {
        EventManager.onCurrentLetterChange.AddListener(UpdateCurrentLetter);
        EventManager.onNextLettersChange.AddListener(UpdateNextLetters);
        EventManager.onPastLettersChange.AddListener(UpdatePastLetters);
        EventManager.onMultiplierChange.AddListener(UpdateMultiplier);
        EventManager.onComboChange.AddListener(UpdateCombo);
    }

    private void UpdateMultiplier(long combo)
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
                tmp += "<color=#" + ColorUtility.ToHtmlStringRGB(successColor) + ">" + pastLetters[i] + "</color>";
            }
            else
            {
                tmp += pastLetters[i];
            }
        }

        previousLettersText.text = tmp;
    }

}
