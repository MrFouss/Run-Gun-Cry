using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventsDebriefing : MonoBehaviour {

    public void OnMainMenuPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
