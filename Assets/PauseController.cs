using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    public Transform pauseMenu;
    public Transform mecha;

    public Slider slider;

    void Start()
    {
        slider = pauseMenu.GetComponentInChildren<Slider>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.gameObject.activeInHierarchy == false)
            {
                // TODO : Fill if necessary
                // Display menu
                pauseMenu.gameObject.SetActive(true);

                // Stop time
                Time.timeScale = 0;

                // Disable controls for the players
                mecha.GetComponent<PilotController>().enabled = false;
                mecha.GetComponent<CannonBehavior>().enabled = false;
                mecha.GetComponent<EngineerLetterTyper>().enabled = false;
                mecha.GetComponent<FollowMouse>().enabled = false;
            }
            else
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                mecha.GetComponent<PilotController>().enabled = true;
                mecha.GetComponent<CannonBehavior>().enabled = true;
                mecha.GetComponent<EngineerLetterTyper>().enabled = true;
                mecha.GetComponent<FollowMouse>().enabled = true;

            }
        }
	}

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        mecha.GetComponent<PilotController>().enabled = true;
        mecha.GetComponent<CannonBehavior>().enabled = true;
        mecha.GetComponent<FollowMouse>().enabled = true;
        mecha.GetComponent<EngineerLetterTyper>().enabled = true;
    }

    public void UpdateVolume()
    {
        AudioListener.volume = slider.value;
    }
}
