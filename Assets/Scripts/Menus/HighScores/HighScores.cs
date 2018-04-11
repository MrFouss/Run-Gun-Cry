using UnityEngine;
using System.IO;

[System.Serializable]
public class HighScores
{
    private static string _highScoreSaveFile = "highScoreSaveFile.json";

    [SerializeField]
    public HighScoresEntry[] Entries;

    public HighScores()
    {
        Entries = new HighScoresEntry[3];
        for (int i = 0; i < 3; ++i)
        {
            Entries[i] = new HighScoresEntry();
        }
    }

    public bool Insert(HighScoresEntry newEntry)
    {
        if (newEntry.TeamScore > Entries[2].TeamScore)
        {
            // insert high score
            Entries[2] = newEntry;
            int i = 1;
            while (i >= 0 && Entries[i].TeamScore < Entries[i + 1].TeamScore)
            {
                HighScoresEntry tmp = Entries[i];
                Entries[i] = Entries[i + 1];
                Entries[i + 1] = tmp;
                i--;
            }
            return true;
        } else
        {
            return false;
        }
    }

    public static HighScores LoadHighScores()
    {
        string saveFilePath = Application.dataPath + Path.DirectorySeparatorChar + _highScoreSaveFile;

        if (File.Exists(saveFilePath))
        {
            return JsonUtility.FromJson<HighScores>(File.ReadAllText(saveFilePath));
        }
        else
        {
            return new HighScores();
        }
    }

    public static void SaveHighScores(HighScores data)
    {
        File.WriteAllText(Application.dataPath + Path.DirectorySeparatorChar + _highScoreSaveFile, JsonUtility.ToJson(data));
    }
}
