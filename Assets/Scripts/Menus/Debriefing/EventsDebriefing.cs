using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventsDebriefing : MonoBehaviour {

    public void Awake() {
        Cursor.visible = true;
    }

    public void OnMainMenuPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
