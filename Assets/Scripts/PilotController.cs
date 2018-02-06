using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{

    private bool isJumping;
    private Vector3 groundCheck;
    private bool isGrounded;
    private LayerMask layerMask;

    public float jumpForce = 5f;
    public float frontAcceleration = 25f;
    public float neutralAcceleration = 2.5f;
    public float brakeAcceleration = 25f;
    public float sidesAcceleration = 75f;
    public float maxSpeedZ = 30f;
    public float maxSpeedX = 50f;
    public float minSpeed = 5f;
    public float airControlMultiplier = 0.25f;
    public float timeToStopStrafing = 0.1f;
   
    private bool cruiseControl = false;
    private float cruiseControlSpeed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void InitValues()
    {
        jumpForce = 5f;
        frontAcceleration = 25f;
        neutralAcceleration = 2.5f;
        brakeAcceleration = 25f;
        sidesAcceleration = 75f;
        maxSpeedZ = 30f;
        maxSpeedX = 50f;
        minSpeed = 5f;
        airControlMultiplier = 0.25f;
        timeToStopStrafing = 0.1f;
        cruiseControl = false;
    }

    private void Start()
    {
        isJumping = false;
        isGrounded = false;
        groundCheck = Vector3.down * 0.5f;
        layerMask = (1 << LayerMask.NameToLayer("Ground"));

        InitValues();

        cruiseControlSpeed = minSpeed;
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
        if(Input.GetButtonDown("CruiseControl"))
        {
            if(Mathf.Abs(rb.velocity.z - cruiseControlSpeed) < 0.5f)
            {
                cruiseControl = !cruiseControl;
            }
            else
            {
                cruiseControl = true;
            }
            if (cruiseControl)
            {
                cruiseControlSpeed = rb.velocity.z;
            }
            else
            {
                cruiseControlSpeed = minSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.VelocityChange);
            isJumping = false;
        }

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        Vector3 updatedVelocity = rb.velocity;
        Debug.Log(rb.velocity);
        if(dirX == 0f)
        {
            Debug.Log(updatedVelocity.x);
            float xVel = updatedVelocity.x - (updatedVelocity.x / timeToStopStrafing) * Time.fixedDeltaTime;
            Debug.Log(xVel);
            updatedVelocity.x = (Mathf.Abs(xVel) <= Mathf.Epsilon ? 0f : xVel);
        }
        else
        {
            float xVel = updatedVelocity.x + dirX * sidesAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : airControlMultiplier);
            updatedVelocity.x = Mathf.Sign(xVel) * Mathf.Min(maxSpeedX, Mathf.Abs(xVel));
        }

        if(dirZ < 0f)
        {
            updatedVelocity.z = Mathf.Max(minSpeed, updatedVelocity.z + dirZ * brakeAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : airControlMultiplier));
            cruiseControl = false;
            cruiseControlSpeed = minSpeed;
        }
        else if(dirZ == 0f)
        {
            updatedVelocity.z += neutralAcceleration * Time.fixedDeltaTime;
        }
        else
        {
            updatedVelocity.z = Mathf.Min(maxSpeedZ, updatedVelocity.z + dirZ * frontAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : airControlMultiplier));
        }

        rb.velocity = updatedVelocity;

        if (rb.velocity.z < cruiseControlSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, cruiseControlSpeed);
        }
    }

}
