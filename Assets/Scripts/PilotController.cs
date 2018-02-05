using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{

    private bool isJumping;
    private Vector3 groundCheck;
    private bool isGrounded;
    private LayerMask layerMask;

    public float jumpForce;
    public float frontAcceleration;
    public float neutralAcceleration;
    public float brakeAcceleration;
    public float sidesAcceleration;
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

    private bool cruiseControl;
    private float cruiseControlSpeed;


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

        frontAcceleration = 25f;
        neutralAcceleration = 2.5f;
        brakeAcceleration = 25f;
        sidesAcceleration = 50f;
        maxSpeed = 30f;
        minSpeed = 5f;
        airControlMultiplier = 0.25f;

        cruiseControl = false;
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
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isJumping = false;
        }

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        Vector3 updatedVelocity = rb.velocity;

        if(dirX == 0f)
        {
            updatedVelocity.x = 0f;
        }
        else
        {
            updatedVelocity.x = Mathf.Min(maxSpeed, updatedVelocity.x + dirX * sidesAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : airControlMultiplier));
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
            updatedVelocity.z = Mathf.Min(maxSpeed, updatedVelocity.z + dirZ * frontAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : airControlMultiplier));
        }

        Debug.Log(updatedVelocity);

        rb.velocity = updatedVelocity;

        if (Velocity > maxSpeed)
        {
            Vector2 v = new Vector2(rb.velocity.x, rb.velocity.z).normalized * maxSpeed;
            rb.velocity = new Vector3(v.x, rb.velocity.y, v.y);
        }

        if (rb.velocity.z < cruiseControlSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, cruiseControlSpeed);
            if(Velocity > maxSpeed)
            {
                rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * Mathf.Sqrt(Mathf.Pow(Velocity, 2) - Mathf.Pow(cruiseControlSpeed, 2)), rb.velocity.y, cruiseControlSpeed); 
            }
        }
    }

}
