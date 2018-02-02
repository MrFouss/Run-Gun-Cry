using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineerLetterTyper : MonoBehaviour
{
    public int NEXT_LETTERS_NBR = 5;
    public int PAST_LETTERS_NBR = 5;

    private char currLetter;
    private List<char> nextLetters = new List<char>();
    private List<char> pastLetters = new List<char>();
    private List<bool> pastLettersSucces = new List<bool>();

    // Use this for initialization
    void Start()
    {
        currLetter = (char)Random.Range('A', 'Z');
        EngineerEventManager.onCurrentLetterChange.Invoke(currLetter);

        for (int i = 0; i < NEXT_LETTERS_NBR; ++i)
        {
            nextLetters.Add((char)Random.Range('A', 'Z'));
        }
        EngineerEventManager.onNextLettersChange.Invoke(nextLetters.ToArray());

        EngineerEventManager.onPastLettersChange.Invoke(new char[] { }, new bool[] { });
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
                EngineerEventManager.onPastLettersChange.Invoke(pastLetters.ToArray(), pastLettersSucces.ToArray());

                // update curr letter
                currLetter = nextLetters[0];
                EngineerEventManager.onCurrentLetterChange.Invoke(currLetter);

                // update next letters
                nextLetters.RemoveAt(0);
                nextLetters.Add((char)Random.Range('A', 'Z'));
                EngineerEventManager.onNextLettersChange.Invoke(nextLetters.ToArray());
            }
        }
    }

}
