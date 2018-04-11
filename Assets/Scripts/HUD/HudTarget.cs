using UnityEngine;
using UnityEngine.UI;

public class HudTarget : MonoBehaviour {

    private Vector2 _targetPosition;

    public Color DefaultColor = Color.black;
    public Color HighlightColor = Color.red;

    public CannonBehavior canon;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public Vector2 TargetPosition
    {
        get
        {
            return _targetPosition;
        }
        set
        {
            _targetPosition = value;
        }
    }

    private void Update()
    {
        transform.position = TargetPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, canon.aimRange) && (info.transform.gameObject.tag == Tags.EnemyChargerTag || info.transform.gameObject.tag == Tags.EnemyLaserTag || info.transform.gameObject.tag == Tags.EnemyCowardTag))
        {
            // Raycast from crosshair position hit either an ennemy charger or an ennemy laser
            _image.color = HighlightColor;
        }
        else
        {
            _image.color = DefaultColor;
        }
    }
}
