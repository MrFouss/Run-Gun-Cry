using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    public RawImage FilterImage;

    public ProgressBar HealthBar;
    public ProgressBar ShieldBar;
    public ProgressBar EnergyBar;
    public Text ScoreText;

    public Image Crosshair;

    private void Awake()
    {
        //EventManager.OnShieldChange.AddListener(shield => ShieldBar.Progress = shield);
        //EventManager.OnHealthChange.AddListener(health => HealthBar.Progress = health);
        //EventManager.OnEnergyChange.AddListener(energy => EnergyBar.Progress = energy);
        //EventManager.OnScoreChange.AddListener(score => ScoreText.text = score.ToString());

        EventManager.Instance.OnFilterSelected.AddListener(UpdateFilter);
        EventManager.onCrosshairPositionChange.AddListener(pos => Crosshair.GetComponent<RectTransform>().position = pos);
    }

    private void UpdateFilter(FilterColor color)
    {
        switch (color)
        {
            case FilterColor.RED:
                FilterImage.color = new Color(1, 0, 0);
                break;
            case FilterColor.GREEN:
                FilterImage.color = new Color(0, 1, 0);
                break;
            case FilterColor.BLUE:
                FilterImage.color = new Color(0, 0, 1);
                break;
        }
    }
}
