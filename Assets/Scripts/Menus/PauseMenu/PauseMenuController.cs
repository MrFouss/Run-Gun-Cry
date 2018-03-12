using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject Mecha;

    private Slider slider;

    void Start()
    {
        slider = PauseMenu.GetComponentInChildren<Slider>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if(PauseMenu.activeInHierarchy == false)
            {
                LoadPauseMenu();
            }
            else
            {
                ResumeGame();
            }
        }
	}

    public void LoadPauseMenu() {
        // Display menu
        PauseMenu.SetActive(true);

        // Show cursor
        Cursor.visible = true;

        // Stop time
        Time.timeScale = 0;

        // Disable controls for the players
        Mecha.GetComponent<PilotController>().enabled = false;
        Mecha.GetComponent<CannonBehavior>().enabled = false;
        Mecha.GetComponent<EngineerLetterTyper>().enabled = false;
        Mecha.GetComponent<FollowMouse>().enabled = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        PauseMenu.gameObject.SetActive(false);

        // Hide cursor
        Cursor.visible = false;

        Time.timeScale = 1;

        Mecha.GetComponent<PilotController>().enabled = true;
        Mecha.GetComponent<CannonBehavior>().enabled = true;
        Mecha.GetComponent<EngineerLetterTyper>().enabled = true;
        Mecha.GetComponent<FollowMouse>().enabled = true;
    }

    public void UpdateVolume()
    {
        AudioListener.volume = slider.value;
    }
}
