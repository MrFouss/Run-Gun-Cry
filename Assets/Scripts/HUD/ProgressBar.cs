using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Image Bar;
    public Image Background;

    // between 0 and 1
    private float _progress = 0;
    public float Progress
    {
        set
        {
            _progress = Mathf.Clamp(value, 0, 1);
            Vector2 scale = Bar.rectTransform.GetComponent<RectTransform>().localScale;
            scale.x = _progress;
            Bar.rectTransform.GetComponent<RectTransform>().localScale = scale;
        }
        get
        {
            return _progress;
        }
    }
}
