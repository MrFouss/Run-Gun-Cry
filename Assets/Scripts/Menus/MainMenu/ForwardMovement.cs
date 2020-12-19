using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour {

    private Rigidbody _rigidbody;
    public float ForwardSpeed = 2;
    public float ForwardRotation = 10;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = Vector3.forward * ForwardSpeed;
        _rigidbody.angularVelocity = Vector3.forward * ForwardRotation;
    }
}
