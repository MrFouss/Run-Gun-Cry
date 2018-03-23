using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour {

    // To be attached to a canvas as seen in the GameOver Scene

    public Text PilotDamageText;
    public Text PilotDistanceText;

    public Text GunnerAccuracyText;
    public Text GunnerDamageText;
    public Text GunnerConsumptionText;

    public Text EngineerAccuracyText;
    public Text EngineerShieldText;
    public Text EngineerEnergyText;
    public Text EngineerTimeWithoutEnergyText;
    public Text EngineerAverageEnergyText;

    public GameObject NewHighScoreGameObject;

    public InputField RunnerNameText;
    public InputField GunnerNameText;
    public InputField EngineerNameText;

    // Use this for initialization
    void Start () {
        
        PilotDamageText.text = "Dégâts subis par collisions : " + Scoring.PilotDamageTaken.ToString();
        PilotDistanceText.text = "Distance parcourue : " + Scoring.PilotDistanceTravelled.ToString();
        GunnerAccuracyText.text = "Précision : " + Scoring.GunnerAccuracy.ToString() + "%";
        GunnerDamageText.text = "Dégâts subis par ennemis : " + Scoring.GunnerDamageTaken.ToString();
        GunnerConsumptionText.text = "Consommation d'énergie : " + Scoring.GunnerEnergyConsumption.ToString();
        EngineerAccuracyText.text = "Précision : " + Scoring.EngineerAccuracy.ToString() + "%";
        EngineerShieldText.text = "Bouclier généré : " + Scoring.EngineerShieldGenerated.ToString();
        EngineerEnergyText.text = "Énergie générée : " + Scoring.EngineerEnergyGenerated.ToString();
        EngineerTimeWithoutEnergyText.text = "Secondes passées sans énergie : " + Scoring.EngineerTimeWithoutEnergy.ToString();
        EngineerAverageEnergyText.text = "Énergie moyenne : " + Scoring.EngineerAverageEnergy.ToString();

        HighScores highScores = HighScores.LoadHighScores();

        if (Scoring.TeamScore > highScores.Entries[2].TeamScore)
        {
            NewHighScoreGameObject.SetActive(true);
        }
	}
    private void OnDestroy()
    {
        HighScores highScores = HighScores.LoadHighScores();
        if (NewHighScoreGameObject.activeSelf)
        {
            HighScoresEntry newEntry = new HighScoresEntry
            {
                EngineerName = EngineerNameText.text,
                GunnerName = GunnerNameText.text,
                RunnerName = RunnerNameText.text,
                TeamScore = Scoring.TeamScore            
            };
            highScores.Insert(newEntry);
            HighScores.SaveHighScores(highScores);
        }
    }
}
