using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HudEyes : MonoBehaviour {

    // get references to assets

    [SerializeField]
    private Sprite _openRedEyeSprite;
    [SerializeField]
    private Sprite _openGreenEyeSprite;
    [SerializeField]
    private Sprite _openBlueEyeSprite;

    [SerializeField]
    private Sprite _closedRedEyeSprite;
    [SerializeField]
    private Sprite _closedGreenEyeSprite;
    [SerializeField]
    private Sprite _closedBlueEyeSprite;

    // get references to scene components

    [SerializeField]
    private Transform _redEyeTransform;
    [SerializeField]
    private Transform _greenEyeTransform;
    [SerializeField]
    private Transform _blueEyeTransform;

    [SerializeField]
    private Image _redEyeImage;
    [SerializeField]
    private Image _greenEyeImage;
    [SerializeField]
    private Image _blueEyeImage;

    // the eyes are placed around this game object's position
    // each eye is 360/3 degree apart from the other eyes

    // the angle at which the selected eye is positionned
    public float SelectedEyeAngle = -120;

    // distance between this game object's position and the eyes
    public float EyeToCenterDistance = 35;

    // number of degrees an eye can travel in 1 second
    public float EyeAngularSpeedPerSecond = 180;

    // the selected eye
    [SerializeField]
    private FilterColor _selectedEye = FilterColor.RED;
    public FilterColor SelectedEye {
        get
        {
            return _selectedEye;
        }
        set
        {
            _selectedEye = value;
        }
    }

    // gather information on each eye
    private struct EyeData
    {
        public EyeData(FilterColor color, Transform transform, Image image)
        {
            Color = color;
            Transform = transform;
            Image = image;
        }

        public FilterColor Color;
        public Transform Transform;
        public Image Image;
    };

    // the list of eyes
    // this queue is used to maintain an ordered list of eyes
    // the first element (peek) is the selected eye
    private Queue<EyeData> _eyeData;

    private void Awake()
    {
        // create the eyes queue
        _eyeData = new Queue<EyeData>
        (
            new EyeData[]
            {
                new EyeData(FilterColor.RED, _redEyeTransform, _redEyeImage),
                new EyeData(FilterColor.GREEN, _greenEyeTransform, _greenEyeImage),
                new EyeData(FilterColor.BLUE, _blueEyeTransform, _blueEyeImage)
            }
        );
    }

    // rotate list until the first eye data (peek) is the selected eye
    private void UpdateEyeDataQueue()
    {
        if (_eyeData == null)
        {
            // TODO find why the editor does not call awake and causes this methode to raise an error
            Awake();
        }
        while(_eyeData.Peek().Color != SelectedEye)
        {
            RotateEyeDataQueue();
        }
    }

    // rotate eye data queue (dequeue and enqueue)
    private void RotateEyeDataQueue()
    {
        _eyeData.Enqueue(_eyeData.Dequeue());
    }

    private void Update () {
        UpdateEyeDataQueue();

        // get selected eye transform
        Transform eye = _eyeData.Peek().Transform;

        // compute the angle between the selected eye and its desired position
        Vector2 centerToEye = (eye.position - transform.position).normalized;
        Vector2 centerToSelection = Quaternion.Euler(0, 0, SelectedEyeAngle) * new Vector2(0, 1);
        float eyeToSelectionAngle = Vector2.SignedAngle(centerToEye, centerToSelection);
        if (Application.isPlaying)
        {
            // take into account the angular speed if playing
            eyeToSelectionAngle = Mathf.Clamp(eyeToSelectionAngle,
                -Time.deltaTime * EyeAngularSpeedPerSecond, Time.deltaTime * EyeAngularSpeedPerSecond);
        }
        
        int i = 0;
        do
        {
            // update eyes positions relative to the selected eye's
            Vector3 translationFromCenter = Quaternion.AngleAxis(eyeToSelectionAngle + i*360.0f/3, new Vector3(0, 0, 1)) * centerToEye * EyeToCenterDistance;
            _eyeData.Peek().Transform.position = transform.position + translationFromCenter;

            // update eye sprite
            switch (_eyeData.Peek().Color)
            {
                case FilterColor.RED:
                    if (_eyeData.Peek().Color == SelectedEye)
                    {
                        _eyeData.Peek().Image.sprite = _openRedEyeSprite;
                    }
                    else
                    {
                        _eyeData.Peek().Image.sprite = _closedRedEyeSprite;
                    }
                    break;
                case FilterColor.GREEN:
                    if (_eyeData.Peek().Color == SelectedEye)
                    {
                        _eyeData.Peek().Image.sprite = _openGreenEyeSprite;
                    }
                    else
                    {
                        _eyeData.Peek().Image.sprite = _closedGreenEyeSprite;
                    }
                    break;
                case FilterColor.BLUE:
                    if (_eyeData.Peek().Color == SelectedEye)
                    {
                        _eyeData.Peek().Image.sprite = _openBlueEyeSprite;
                    }
                    else
                    {
                        _eyeData.Peek().Image.sprite = _closedBlueEyeSprite;
                    }
                    break;
            }

            // rotate eye data queue
            i++;
            RotateEyeDataQueue();
        } while (_eyeData.Peek().Color != SelectedEye);
    }
}
