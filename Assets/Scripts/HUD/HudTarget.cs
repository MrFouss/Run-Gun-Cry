using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTarget : MonoBehaviour {

    private Vector2 _targetPosition;
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
    }
}
