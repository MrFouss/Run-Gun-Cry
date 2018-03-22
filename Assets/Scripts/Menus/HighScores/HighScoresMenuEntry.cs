using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresMenuEntry : MonoBehaviour {
    
    public HighScoresEntry Entry = new HighScoresEntry();

    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _scoreText;

    private void Update()
    {
        _nameText.text = Entry.PlayerName;
        _scoreText.text = Entry.PlayerScore.ToString();
    }
}
