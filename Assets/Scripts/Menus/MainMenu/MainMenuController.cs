using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {

    public GameObject MainMenuContainer;
    public GameObject CreditsContainer;

    public void OnStartGamePress()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OnHighScorePress()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void OnCreditsPress()
    {
        MainMenuContainer.SetActive(false);
        CreditsContainer.SetActive(true);
    }

    public void OnCreditsBackToMainMenuPress()
    {
        CreditsContainer.SetActive(false);
        MainMenuContainer.SetActive(true);
    }

    public void OnQuitGamePress()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
