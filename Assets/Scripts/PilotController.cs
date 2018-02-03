using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{

    private bool isJumping;
    public float jumpForce;
    private Vector3 groundCheck;
    private bool isGrounded;
    private LayerMask layerMask;

    public float frontAccelerationForce;
    public float backwardAccelerationForce;
    public float sidesAccelerationForce;
    public float maxSpeed;
    public float minSpeed;
    public float airControlMultiplier;
    public float Velocity
    {
        get
        {
            return Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2));
        }
    }


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isJumping = false;
        isGrounded = false;
        groundCheck = Vector3.down * 0.5f;
        layerMask = (1 << LayerMask.NameToLayer("Ground"));

        frontAccelerationForce = 100f;
        backwardAccelerationForce = 50f;
        sidesAccelerationForce = 100f;
        maxSpeed = 20f;
        minSpeed = 5f;
        airControlMultiplier = 0.25f;
    }

    private void Update()
    {
        isGrounded = Physics.Linecast(transform.position, transform.position + groundCheck, layerMask);
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJumping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isJumping = false;
        }

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        if(dirZ >= 0f)
        {
            rb.AddForce(new Vector3(dirX * sidesAccelerationForce, 0f, dirZ * frontAccelerationForce) * (isGrounded ? 1f : airControlMultiplier));
        }
        else
        {
            rb.AddForce(new Vector3(dirX * sidesAccelerationForce, 0f, dirZ * backwardAccelerationForce) * (isGrounded ? 1f : airControlMultiplier));
        }
        

        if (Velocity > maxSpeed)
        {
            Vector2 v = new Vector2(rb.velocity.x, rb.velocity.z).normalized * maxSpeed;
            rb.velocity = new Vector3(v.x, rb.velocity.y, v.y);
        }

        if (rb.velocity.z < minSpeed)
        {
            if (rb.velocity.x == 0f)
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, minSpeed);
            }
            else if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector3(Mathf.Sqrt(Mathf.Abs(Mathf.Pow(Velocity, 2) - Mathf.Pow(minSpeed, 2))), rb.velocity.y, minSpeed);
            }
            else
            {
                rb.velocity = new Vector3(-Mathf.Sqrt(Mathf.Abs(Mathf.Pow(Velocity, 2) - Mathf.Pow(minSpeed, 2))), rb.velocity.y, minSpeed);
            }
        }
    }

}
