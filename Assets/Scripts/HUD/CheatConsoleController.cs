using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatConsoleController : MonoBehaviour {

    public GameObject ConsoleHUD;
    public GameObject Mecha;
    public Text History;

    private bool openConsole;
    private InputField inputField;
    private string cheatEntered;
    

    // Use this for initialization
    void Start () {
        inputField = ConsoleHUD.GetComponent<InputField>();
        openConsole = false;
        ConsoleHUD.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("DeveloperConsole"))
        {
            if (!openConsole)
            {
                openConsoleAndPauseGame();
                
            } else
            {
                resumeGame();
            }
        }
	}

    private void openConsoleAndPauseGame()
    {
        History.text = "Run Gun Cry - Copyright (c) 2018";
        openConsole = true;
        ConsoleHUD.SetActive(true);

        // Show cursor
        Cursor.visible = true;

        // Stop time
        Time.timeScale = 0;

        // Disable controls for the players
        Mecha.GetComponent<PilotController>().enabled = false;
        Mecha.GetComponent<CannonBehavior>().enabled = false;
        Mecha.GetComponent<EngineerLetterTyper>().enabled = false;
        Mecha.GetComponent<FollowMouse>().enabled = false;

        inputField.ActivateInputField();
    }

    private void resumeGame()
    {
        openConsole = false;
        ConsoleHUD.SetActive(false);

        Cursor.visible = false;

        Time.timeScale = 1;

        Mecha.GetComponent<PilotController>().enabled = true;
        Mecha.GetComponent<CannonBehavior>().enabled = true;
        Mecha.GetComponent<EngineerLetterTyper>().enabled = true;
        Mecha.GetComponent<FollowMouse>().enabled = true;
    }

    public void GetInput(string inputString)
    {
        if(inputString != "")
        {
            cheatEntered = inputString;
            History.text = History.text + "\n" + "> " + inputString;
            inputField.text = "";
            ActivateCheat();
            inputField.ActivateInputField();
        }
    }

    private void ActivateCheat()
    {
        switch (cheatEntered)
        {
            case "ZordonIsLove":
                History.text = History.text + "\n" + "Cheat code detected : infinite health";
                Mecha.GetComponent<MechaController>().MaxHealth = 1000000;
                Mecha.GetComponent<MechaController>().Health = 1000000;
                break;
            case "Palpatine":
                History.text = History.text + "\n" + "Cheat code detected : infinite energy";
                Mecha.GetComponent<MechaController>().MaxEnergy = 1000000;
                Mecha.GetComponent<MechaController>().Energy = 1000000;
                break;
            default:
                History.text = History.text + "\n" + "Unknown command";
                break;
        }
    }

}
