using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineerLetterTyper : MonoBehaviour
{
    public int NextLettersNumber = 5;
    public int PastLettersNumber = 5;

    private char currLetter;
    private List<char> nextLetters = new List<char>();
    private List<char> pastLetters = new List<char>();
    private List<bool> pastLettersSuccess = new List<bool>();

    // Use this for initialization
    void Start()
    {
        // randomly init current and next letters

        currLetter = (char)Random.Range('A', 'Z');
        EventManager.onCurrentLetterChange.Invoke(currLetter);

        for (int i = 0; i < NextLettersNumber; ++i)
        {
            nextLetters.Add((char)Random.Range('A', 'Z'));
        }
        EventManager.onNextLettersChange.Invoke(nextLetters.ToArray());

        EventManager.onPastLettersChange.Invoke(new char[] { }, new bool[] { });
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            char upperC = char.ToUpper(c);
            if (upperC >= 'A' && upperC <= 'Z')
            {
                if (upperC == currLetter)
                {
                    // correct letter entered
                    pastLettersSuccess.Add(true);
                    EventManager.onLetterTyped.Invoke(upperC, true);
                }
                else
                {
                    // wrong letter entered
                    pastLettersSuccess.Add(false);
                    EventManager.onLetterTyped.Invoke(upperC, false);
                }
                
                // update past letters
                pastLetters.Add(currLetter);
                if (pastLetters.Count > PastLettersNumber)
                {
                    pastLetters.RemoveAt(0);
                    pastLettersSuccess.RemoveAt(0);
                }
                EventManager.onPastLettersChange.Invoke(pastLetters.ToArray(), pastLettersSuccess.ToArray());

                // update curr letter
                currLetter = nextLetters[0];
                EventManager.onCurrentLetterChange.Invoke(currLetter);

                // update next letters
                nextLetters.RemoveAt(0);
                nextLetters.Add((char)Random.Range('A', 'Z'));
                EventManager.onNextLettersChange.Invoke(nextLetters.ToArray());
            }
        }
    }

}
