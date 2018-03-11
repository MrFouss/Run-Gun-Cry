using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour {
    
    // progress bar advance towards this direction
    public Direction Direction = Direction.LEFT;

    // progress bar speed value change speed
    // this induces a delay between the moment the progress is set and the moment it actually shows that progress
    public float ProgressSpeedPerSecond = 1;

    [SerializeField]
    private float _progress = 1;
    // target progress value (between 0 and 1)
    public float Progress
    {
        get
        {
            return _progress;
        }
        set
        {
            _progress = value;
        }
    }

    // actual progress displayed (may be different from Progress due to progress speed)
    [SerializeField]
    private float _actualProgress = 1;

    // a sprite of the shape of the progress bar (typically a white shape on a transparent background)
    public Sprite BarMask;

    // the color of the progress bar
    public Color BarColor;
    public Color AltBarColor;

    // alternate between base and alternative bar color at a certain rate
    [SerializeField]
    private bool _blinkingColor = false;
    public bool BlinkingColor
    {
        set
        {
            _blinkingColor = value;
        }
        get
        {
            return _blinkingColor;
        }
    }
    public float BlinkingDuration = 0.5f;

    // keep track of the blinking state
    private bool _currentlyBlinking = false;

    // the color of the progress bar background
    public Color BackgroundColor;

    // references to components

    [SerializeField]
    private RectTransform _progressMaskRect;
    [SerializeField]
    private RectTransform _progressBarRect;
    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private Image _barImage;

    private void SetBarColor()
    {
        // decide what to do
        if (BlinkingColor && !_currentlyBlinking)
        {
            // should start blinking
            _currentlyBlinking = true;
            SetBarAltColor();
        }
        else if (!BlinkingColor && _currentlyBlinking)
        {
            // should stop blinking
            _currentlyBlinking = false;
            SetBarBaseColor();
        }
        else if (BlinkingColor && _currentlyBlinking)
        {
            // in blinking mode
            // don't do anything
        }
        else if (!BlinkingColor && !_currentlyBlinking)
        {
            // out of blinking mode
            SetBarBaseColor();
        }
    }

    private void SetBarBaseColor()
    {
        // set base color
        _barImage.color = BarColor;
        if (_currentlyBlinking && Application.isPlaying)
        {
            // continue blinking in x seconds
            Invoke("SetBarAltColor", BlinkingDuration);
        }
    }

    private void SetBarAltColor()
    {
        if (_currentlyBlinking && Application.isPlaying)
        {
            // change color and continue invocations only if in blinking mode
            _barImage.color = AltBarColor;
            Invoke("SetBarBaseColor", BlinkingDuration);
        }
        else if (_currentlyBlinking)
        {
            _barImage.color = AltBarColor;
        }
    }

    private void Update()
    {
        // blink color or base color
        SetBarColor();
        _barImage.sprite = BarMask;
        _backgroundImage.sprite = BarMask;
        _backgroundImage.color = BackgroundColor;
        
        // update actual progress
        if (!Application.isPlaying)
        {
            // synchronize actual progress and progress
            _actualProgress = Progress;
        } else
        {
            // take into account the progress speed
            _actualProgress += Mathf.Clamp(Progress - _actualProgress, -Time.deltaTime * ProgressSpeedPerSecond, Time.deltaTime * ProgressSpeedPerSecond);
        }

        // reset progress bar to maximum size
        _progressMaskRect.offsetMax = Vector2.zero;
        _progressMaskRect.offsetMin = Vector2.zero;
        _progressBarRect.offsetMin = Vector2.zero;
        _progressBarRect.offsetMax = Vector2.zero;

        // get max bar size
        Vector2 size = _progressMaskRect.rect.size;

        // update offsets
        // the progress mask is dimensionned to show the actual progress
        // the progress bar's dimension is increased to counter its parent's (the mask's) reduced dimension 
        switch (Direction)
        {
            case Direction.LEFT:
                _progressMaskRect.offsetMin = new Vector2(size.x * (1 - _actualProgress), 0);
                _progressBarRect.offsetMin = new Vector2(-size.x * (1 - _actualProgress), 0);
                break;
            case Direction.RIGHT:
                _progressMaskRect.offsetMax = new Vector2(-size.x * (1 - _actualProgress), 0);
                _progressBarRect.offsetMax = new Vector2(size.x * (1 - _actualProgress), 0);
                break;
            case Direction.UP:
                _progressMaskRect.offsetMax = new Vector2(0, -size.y * (1 - _actualProgress));
                _progressBarRect.offsetMax = new Vector2(0, size.y * (1 - _actualProgress));
                break;
            case Direction.DOWN:
                _progressMaskRect.offsetMin = new Vector2(0, size.y * (1 - _actualProgress));
                _progressBarRect.offsetMin = new Vector2(0, -size.y * (1 - _actualProgress));
                break;
        }
    }
}
