using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {

    public AudioSource AudioSource;


    public void OnStartGamePress()
    {
        SceneManager.LoadScene("TestScene");    
    }

    public void OnHighScorePress()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void OnQuitGamePress()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
