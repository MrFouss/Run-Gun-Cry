using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float MaxSpeed = 20f;
    public float MoveForce = 15f;
    public float MinRange = 10;
    public float DetectionRange = 40f;
    public float ObstacleDetectionRange = 10f;

    public Animation StaticAnimation;
    public Animation MovingAnimation;
    
    private float slowDownMultiplicator = 0.9f;
    private float shootingRange;
    private bool isInRange = false;
    private Transform mecha = null;
    private bool foundMecha = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LookForMecha();
        shootingRange = GetComponentInChildren<SphereCollider>().radius;
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

    private void Update()
    {
        if (!foundMecha)
        {
            LookForMecha();
        }
        else
        {
            //TODO : Uncomment when animations is available
            //if (isMoving)
            //{
            //    MovingAnimation.Play();
            //}
        }
    }

    private void Stop()
    {
        rb.velocity *= slowDownMultiplicator;
        //TODO : Uncomment when animation is available
        //StaticAnimation.Play();
    }
    

    private void FixedUpdate()
    {
        if (foundMecha)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward * (rb.velocity.z > 0 ? 1f : -1f), out hit, ObstacleDetectionRange))
            {
                if (hit.collider.CompareTag(Tags.ObstacleWallTag))
                {
                    Stop();
                }
            }
            else
            {
                if (isInRange)
                {
                    float distance = Mathf.Abs(transform.position.z - mecha.position.z);
                    if (distance <= MinRange)
                    {
                        rb.AddForce(Vector3.forward * MoveForce);
                    }
                    else if (distance > shootingRange)
                    {
                        transform.LookAt(mecha.position);
                        rb.AddForce(transform.forward * MoveForce);
                    }
                    else
                    {
                        Stop();
                    }
                    if (rb.velocity.magnitude > MaxSpeed)
                    {
                        rb.velocity = rb.velocity.normalized * MaxSpeed;
                    }
                }
                else
                {
                    float distance = Vector3.Distance(transform.position, mecha.position);
                    isInRange = distance <= DetectionRange;
                }
            }
        }
    }
}
