using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class HighScoresMenu : MonoBehaviour
{
    private static string _saveFile = "highScoreSaveFile.json";
    private HighScores _highScoresData = new HighScores();

    [SerializeField]
    private HighScoresMenuEntry[] _runnerMenuEntries;

    [SerializeField]
    private HighScoresMenuEntry[] _gunnerMenuEntries;

    [SerializeField]
    private HighScoresMenuEntry[] _engineerMenuEntries;

    void Start()
    {
        _highScoresData = HighScores.LoadHighScores(_saveFile);
    }

    private void Update()
    {
        for (int i = 0; i < 3; ++i)
        {
            _runnerMenuEntries[i].Entry = _highScoresData.RunnerEntries[i];
            _gunnerMenuEntries[i].Entry = _highScoresData.GunnerEntries[i];
            _engineerMenuEntries[i].Entry = _highScoresData.EngineerEntries[i];
        }
    }
}
