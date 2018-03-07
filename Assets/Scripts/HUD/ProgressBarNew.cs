using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBarNew : MonoBehaviour {

    public enum Direction {LEFT, RIGHT, UP, DOWN};
    
    // TODO naming convention
    public Direction direction = Direction.LEFT;
    public float ProgressPerSecond = 1;
    public float Progress = 1;
    [SerializeField]
    private float ActualProgress = 1;

    public Sprite BarMask;
    public Color BarColor;

    [SerializeField]
    private RectTransform ProgressMask;
    [SerializeField]
    private RectTransform ProgressBar;
    [SerializeField]
    private Image BackgroundImage;
    [SerializeField]
    private Image BarImage;

    private void Update()
    {
        // force sprite in editor
        BackgroundImage.sprite = BarMask;
        BarImage.sprite = BarMask;
        BarImage.color = BarColor;
        
        // update actual progress
        if (Application.isEditor && !Application.isPlaying)
        {
            ActualProgress = Progress;
        } else
        {
            ActualProgress += Mathf.Clamp(Progress - ActualProgress, -Time.deltaTime * ProgressPerSecond, Time.deltaTime * ProgressPerSecond);
        }

        // reset progress bar to maximum size
        ProgressMask.offsetMax = Vector2.zero;
        ProgressMask.offsetMin = Vector2.zero;
        ProgressBar.offsetMin = Vector2.zero;
        ProgressBar.offsetMax = Vector2.zero;

        // get max bar size
        Vector2 size = ProgressMask.rect.size;

        // update offsets
        switch (direction)
        {
            case Direction.LEFT:
                ProgressMask.offsetMin = new Vector2(size.x * (1 - ActualProgress), 0);
                ProgressBar.offsetMin = new Vector2(-size.x * (1 - ActualProgress), 0);
                break;
            case Direction.RIGHT:
                ProgressMask.offsetMax = new Vector2(-size.x * (1 - ActualProgress), 0);
                ProgressBar.offsetMax = new Vector2(size.x * (1 - ActualProgress), 0);
                break;
            case Direction.UP:
                ProgressMask.offsetMax = new Vector2(0, -size.y * (1 - ActualProgress));
                ProgressBar.offsetMax = new Vector2(0, size.y * (1 - ActualProgress));
                break;
            case Direction.DOWN:
                ProgressMask.offsetMin = new Vector2(0, size.y * (1 - ActualProgress));
                ProgressBar.offsetMin = new Vector2(0, -size.y * (1 - ActualProgress));
                break;
        }
    }
}
