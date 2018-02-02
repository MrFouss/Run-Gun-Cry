using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersDisplay : MonoBehaviour {

    public Text currentLetterText;
    public Text previousLettersText;
    public Text nextLettersText;
    public Text comboText;

    public Color successColor = Color.green;

    // Use this for initialization
    void Awake () {
        EventManager.onCurrentLetterChange.AddListener(UpdateCurrentLetter);
        EventManager.onNextLettersChange.AddListener(UpdateNextLetters);
        EventManager.onPastLettersChange.AddListener(UpdatePastLetters);
        EventManager.onComboMultiplierChange.AddListener(UpdateComboMultiplier);
    }

    private void UpdateComboMultiplier(long combo, long maxCombo)
    {
        Color color = Color.HSVToRGB((float)0.9f*combo / maxCombo, 1f, 0.8f);
        comboText.text = "<b><color=#" + ColorUtility.ToHtmlStringRGB(color) +">x" + combo + "</color></b>";
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
