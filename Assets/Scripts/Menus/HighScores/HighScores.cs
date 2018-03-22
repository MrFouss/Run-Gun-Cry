using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class HighScores : MonoBehaviour
{
    public HighScoresEntry[] RunnerEntries = new HighScoresEntry[3];
    public HighScoresEntry[] GunnerEntries = new HighScoresEntry[3];
    public HighScoresEntry[] EngineerEntries = new HighScoresEntry[3];

    public static HighScores LoadHighScores(string saveFile)
    {
        string saveFilePath = Application.dataPath + saveFile;

        if (File.Exists(saveFilePath))
        {
            return JsonUtility.FromJson<HighScores>(File.ReadAllText(saveFilePath));
        }
        else
        {
            return new HighScores();
        }
    }

    public static void SaveHighScores(string saveFile, HighScores data)
    {
        File.WriteAllText(Application.dataPath + saveFile, JsonUtility.ToJson(data));
    }
}
