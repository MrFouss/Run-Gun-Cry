using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour {

    public float gravityFactor = -9.81f;

    private Transform objectTransform;
    private ConstantForce objectForce;

	// Use this for initialization
	void Start () {
        objectTransform = GetComponent<Transform>();
        objectForce = GetComponent<ConstantForce>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 directionVector = -1.0f * objectTransform.position;
        directionVector.Normalize();

        objectForce.force = directionVector * gravityFactor;

        objectTransform.Rotate(objectTransform.forward, Vector3.SignedAngle(objectTransform.up, directionVector, objectTransform.forward));
	}
}
