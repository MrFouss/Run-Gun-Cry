using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresMenuEntry : MonoBehaviour {

    public HighScoresEntry Entry;

    [SerializeField]
    private Text _runnerNameText;
    [SerializeField]
    private Text _gunnerNameText;
    [SerializeField]
    private Text _engineerNameText;
    [SerializeField]
    private Text _teamScoreText;

    private void Start()
    {
        Entry = new HighScoresEntry();
    }

    private void Update()
    {
        _runnerNameText.text = Entry.RunnerName;
        _gunnerNameText.text = Entry.GunnerName;
        _engineerNameText.text = Entry.EngineerName;
        _teamScoreText.text = Entry.TeamScore.ToString();
    }
}
