using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCowardController : MonoBehaviour
{

    public float MaxSpeed = 20f;
    public float Acceleration = 20f;
    public float MinDistance = 10f;
    private float minDistanceSqr;
    public float SafeDistance = 1f;
    private float safeDistanceSqr;

    private bool foundMecha = false;
    private Transform mecha = null;
    private float slowDownMultiplicator = 0.9f;
    private Vector3 mousePos;
    private Rigidbody rb;

    private void Start()
    {
        safeDistanceSqr = Mathf.Pow(SafeDistance, 2);
        minDistanceSqr = Mathf.Pow(MinDistance, 2);
        rb = GetComponent<Rigidbody>();
    }

    private void LookForMecha()
    {
        GameObject mecha = GameObject.FindGameObjectWithTag(Tags.MechaBodyTag);
        if (mecha != null)
        {
            this.mecha = GameObject.FindGameObjectWithTag(Tags.MechaBodyTag).transform;
            foundMecha = true;
        }
    }

    private void Stop()
    {
        rb.velocity *= slowDownMultiplicator;
    }

    private void Update()
    {
        if (!foundMecha)
        {
            LookForMecha();
        }
    }

    private void FixedUpdate()
    {
        Vector3 mousePosInScreen = Input.mousePosition;
        mousePosInScreen.z = Vector3.Distance(transform.position, Camera.main.transform.position);
        mousePos = Camera.main.ScreenToWorldPoint(mousePosInScreen);
        Vector3 direction = transform.position - mousePos;
        Debug.Log("transform = " + transform.position + " -- mousepos = " + mousePos + " --  is in safe zone = " + (Vector3.SqrMagnitude(direction) > safeDistanceSqr));
        if (Vector3.SqrMagnitude(direction) <= safeDistanceSqr)
        {
            rb.velocity += direction.normalized * Acceleration * Time.fixedDeltaTime;
        }
        else
        {
            Stop();
        }
        if (foundMecha)
        {
            if (Vector3.SqrMagnitude(mecha.position - transform.position) <= minDistanceSqr)
            {
                rb.velocity += Vector3.forward * Acceleration * Time.fixedDeltaTime;
            }
        }
        if (rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }
    }

}
