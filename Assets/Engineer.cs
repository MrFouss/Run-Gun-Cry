using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engineer : MonoBehaviour
{
    public Text lettersText;

    public int NEXT_LETTERS_NBR = 5;
    public int PAST_LETTERS_NBR = 5;
    public Color successColor = new Color(0.5f, 0.5f, 0.5f);

    private char currLetter;
    private List<char> nextLetters = new List<char>();
    private List<char> pastLetters = new List<char>();
    private List<bool> pastLettersSucces = new List<bool>();

    // Use this for initialization
    void Start()
    {
        lettersText.supportRichText = true;
        InitLetters();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            Debug.Log("Entered " + c);
            char upperC = char.ToUpper(c);
            if (upperC >= 'A' && upperC <= 'Z')
            {
                if (upperC == currLetter)
                {
                    // correct letter entered
                    pastLettersSucces.Add(true);
                }
                else
                {
                    // wrong letter entered
                    pastLettersSucces.Add(false);
                    
                }
                
                // update past letters
                pastLetters.Add(currLetter);
                if (pastLetters.Count > PAST_LETTERS_NBR)
                {
                    pastLetters.RemoveAt(0);
                    pastLettersSucces.RemoveAt(0);
                }

                // update curr letter
                currLetter = nextLetters[0];

                // update next letters
                nextLetters.RemoveAt(0);
                nextLetters.Add((char)Random.Range('A', 'Z'));

                UpdateText();
            }
        }
        //Event.current.
    }

    private void UpdateText()
    {
        string tmp = "";

        for (int i = 0; i < pastLetters.Count; ++i)
        {
            if (pastLettersSucces[i])
            {
                tmp += "<color=#" + ColorUtility.ToHtmlStringRGB(successColor) + ">" + pastLetters[i] + "</color>";
            }
            else
            {
                tmp += pastLetters[i];
            }
        }

        if (pastLetters.Count != 0)
        {
            tmp += " ";
        }

        tmp += currLetter + " ";

        foreach (char c in nextLetters)
        {
            tmp += c;
        }

        lettersText.text = tmp;
    }

    private void InitLetters()
    {
        currLetter = (char)Random.Range('A', 'Z');

        for (int i = 0; i < NEXT_LETTERS_NBR; ++i)
        {
            nextLetters.Add((char)Random.Range('A', 'Z'));
        }

        UpdateText();
    }
}
