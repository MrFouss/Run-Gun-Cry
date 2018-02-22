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
        EventManager.onShieldChange.AddListener(shield => ShieldBar.Progress = shield);
        EventManager.onHealthChange.AddListener(health => HealthBar.Progress = health);
        EventManager.onEnergyChange.AddListener(energy => EnergyBar.Progress = energy);
        EventManager.onScoreChange.AddListener(score => ScoreText.text = score.ToString());

        EventManager.onFilterSelected.AddListener(UpdateFilter);
        EventManager.onCrosshairPositionChange.AddListener(pos => Crosshair.GetComponent<RectTransform>().position = pos);
    }

    private void UpdateFilter(FilterSelector.FilterColor color)
    {
        switch (color)
        {
            case FilterSelector.FilterColor.RED:
                FilterImage.color = new Color(1, 0, 0);
                break;
            case FilterSelector.FilterColor.GREEN:
                FilterImage.color = new Color(0, 1, 0);
                break;
            case FilterSelector.FilterColor.BLUE:
                FilterImage.color = new Color(0, 0, 1);
                break;
        }
    }
}
