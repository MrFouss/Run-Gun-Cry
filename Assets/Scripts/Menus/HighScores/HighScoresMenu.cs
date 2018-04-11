using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresMenu : MonoBehaviour
{
    private HighScores _highScoresData;

    [SerializeField]
    private HighScoresMenuEntry[] _menuEntries;

    private void Start()
    {
        _highScoresData = HighScores.LoadHighScores();
    }

    private void Update()
    {
        for (int i = 0; i < 3; ++i)
        {
            _menuEntries[i].Entry = _highScoresData.Entries[i];
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
