using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HudEyes : MonoBehaviour {

    public enum EyeColor { RED, GREEN, BLUE };

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

    public EyeColor SelectedEye = EyeColor.RED;
    public float SelectedAngle;
    public float EyeToCenterDistance;
    public float RotationSpeedPerSecond = 30;
    private float _actualAngle;

    private Queue<EyeColor> _eyeColors;
    private Queue<Transform> _eyeTransforms;
    private Queue<Image> _eyeImages;

    private void Awake()
    {
        Debug.Log("Awake");
        _eyeColors = new Queue<EyeColor>(new EyeColor[]{ EyeColor.RED, EyeColor.GREEN, EyeColor.BLUE });
        _eyeTransforms = new Queue<Transform>(new Transform[] { _redEyeTransform, _greenEyeTransform, _blueEyeTransform });
        _eyeImages = new Queue<Image>(new Image[] { _redEyeImage, _greenEyeImage, _blueEyeImage });
    }

    private void UpdateLists()
    {
        while(_eyeColors.Peek() != SelectedEye)
        {
            RotateLists();
        }
    }

    private void RotateLists()
    {
        _eyeColors.Enqueue(_eyeColors.Dequeue());
        _eyeTransforms.Enqueue(_eyeTransforms.Dequeue());
        _eyeImages.Enqueue(_eyeImages.Dequeue());
    }

    private void Update () {
        UpdateLists();

        // get selected eye
        Transform eye = _eyeTransforms.Peek();

        // compute selected eye rotation
        Vector2 centerToEye = (eye.position - transform.position).normalized;
        Vector2 centerToSelection = Quaternion.Euler(0, 0, SelectedAngle) * new Vector2(0, 1);
        //Debug.Log("selection vector " + centerToSelection);
        float eyeToSelectionAngle = Vector2.SignedAngle(centerToEye, centerToSelection);
        //Debug.Log("angle to selection " + eyeToSelectionAngle);
        if (Application.isPlaying)
        {
            //Debug.Log("non clamp " + eyeToSelectionAngle);
            eyeToSelectionAngle = Mathf.Clamp(eyeToSelectionAngle,
                -Time.deltaTime * RotationSpeedPerSecond, Time.deltaTime * RotationSpeedPerSecond);
            //Debug.Log("clamp " + eyeToSelectionAngle);
        }

        // position eyes relative to the selected eye rotation
        int i = 0;
        do
        {
            Vector3 eyeTranslation = Quaternion.AngleAxis(eyeToSelectionAngle + i*360.0f/3, new Vector3(0, 0, 1)) * centerToEye;

            //Debug.Log("eye translation " + eyeTranslation + " from " + centerToEye + " to " + centerToSelection);
            eyeTranslation = eyeTranslation * EyeToCenterDistance;
            _eyeTransforms.Peek().position = transform.position + eyeTranslation;

            if (_eyeColors.Peek() == SelectedEye)
            {
                switch (_eyeColors.Peek())
                {
                    case EyeColor.RED:
                        _eyeImages.Peek().sprite = _openRedEyeSprite;
                        break;
                    case EyeColor.GREEN:
                        _eyeImages.Peek().sprite = _openGreenEyeSprite;
                        break;
                    case EyeColor.BLUE:
                        _eyeImages.Peek().sprite = _openBlueEyeSprite;
                        break;
                }
            } else
            {
                switch (_eyeColors.Peek())
                {
                    case EyeColor.RED:
                        _eyeImages.Peek().sprite = _closedRedEyeSprite;
                        break;
                    case EyeColor.GREEN:
                        _eyeImages.Peek().sprite = _closedGreenEyeSprite;
                        break;
                    case EyeColor.BLUE:
                        _eyeImages.Peek().sprite = _closedBlueEyeSprite;
                        break;
                }
            }

            i++;
            RotateLists();
        } while (_eyeColors.Peek() != SelectedEye);
    }
}
