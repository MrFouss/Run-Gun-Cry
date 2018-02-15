using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    public ProgressBar HealthBar;
    public ProgressBar ShieldBar;
    public ProgressBar EnergyBar;
    public Text ScoreText;

    private void Awake()
    {
        EventManager.onShieldChange.AddListener(shield => ShieldBar.Progress = shield);
        EventManager.onHealthChange.AddListener(health => HealthBar.Progress = health);
        EventManager.onEnergyChange.AddListener(energy => EnergyBar.Progress = energy);
        EventManager.onScoreChange.AddListener(score => ScoreText.text = score.ToString());
    }
}
