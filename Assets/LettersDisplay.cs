using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersDisplay : MonoBehaviour {

    public Text currentLetterText;
    public Text previousLettersText;
    public Text nextLettersText;

    public Color successColor = Color.green;

    // Use this for initialization
    void Awake () {
        EngineerEventManager.onCurrentLetterChange.AddListener(UpdateCurrentLetter);
        EngineerEventManager.onNextLettersChange.AddListener(UpdateNextLetters);
        EngineerEventManager.onPastLettersChange.AddListener(UpdatePastLetters);
    }

    void UpdateCurrentLetter(char newLetter)
    {
        currentLetterText.text = newLetter.ToString();
    }

    void UpdateNextLetters(char[] nextLetters)
    {
        string tmp = "";

        for (int i = 0; i < nextLetters.Length; ++i)
        {
            tmp += nextLetters[i];
        }

        nextLettersText.text = tmp;
    }

    void UpdatePastLetters(char[] pastLetters, bool[] success)
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
