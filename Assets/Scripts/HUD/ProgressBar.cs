using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Image bar;
    public Image background;

    // between 0 and 1
    private float _Progress = 0;
    public float Progress
    {
        set
        {
            _Progress = Mathf.Clamp(value, 0, 1);
            Vector2 scale = bar.rectTransform.GetComponent<RectTransform>().localScale;
            scale.x = _Progress;
            bar.rectTransform.GetComponent<RectTransform>().localScale = scale;
        }
        get
        {
            return _Progress;
        }
    }
}
