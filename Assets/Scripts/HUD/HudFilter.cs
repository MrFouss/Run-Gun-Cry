using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HudFilter : MonoBehaviour {

    public Color RedColor = new Color(1, 0, 0, 0.5f);
    public Color GreenColor = new Color(0, 1, 0, 0.5f);
    public Color BlueColor = new Color(0, 0, 1, 0.5f);

    [SerializeField]
    private FilterColor _filterColor = FilterColor.RED;
    public FilterColor FilterColor
    {
        get
        {
            return _filterColor;
        }
        set
        {
            _filterColor = value;
        }
    }

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        switch (FilterColor)
        {
            case FilterColor.RED:
                image.color = RedColor;
                break;
            case FilterColor.GREEN:
                image.color = GreenColor;
                break;
            case FilterColor.BLUE:
                image.color = BlueColor;
                break;
        }
    }
}
