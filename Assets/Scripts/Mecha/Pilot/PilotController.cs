using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{

    private bool isJumping;
    private Vector3 groundCheck;
    private bool isGrounded;
    private LayerMask layerMask;

    public float JumpForce = 5f;
    public float FrontAcceleration = 25f;
    public float NeutralAcceleration = 2.5f;
    public float BrakeAcceleration = 25f;
    public float SidesAcceleration = 75f;
    public float MaxNormalSpeedZ = 30f;
    public float MaxZeroEnergySpeedZ = 20f;
    private float MaxSpeedZ = 30f;
    public float MaxSpeedX = 50f;
    public float MinSpeed = 5f;
    public float AirControlMultiplier = 0.25f;
    public float TimeToStopStrafing = 0.1f;
   
    private bool cruiseControl = false;
    private float cruiseControlSpeed;

    private Rigidbody rb;

    private MechaController mechaController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mechaController = GetComponent<MechaController>();
    }

    private void Start()
    {
        isJumping = false;
        isGrounded = false;
        groundCheck = Vector3.down * 0.5f;
        layerMask = (1 << LayerMask.NameToLayer("Ground"));

        cruiseControlSpeed = MinSpeed;
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
                cruiseControlSpeed = MinSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (mechaController.Energy == 0)
        {
            MaxSpeedZ = MaxZeroEnergySpeedZ;
        }
        else
        {
            MaxSpeedZ = MaxNormalSpeedZ;
        }

        if (isJumping)
        {
            // TODO edit for tubular gravity
            rb.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.VelocityChange);
            isJumping = false;
        }

        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");

        Vector3 updatedVelocity = rb.velocity;
        if(dirX == 0f)
        {
            float xVel = updatedVelocity.x - (updatedVelocity.x / TimeToStopStrafing) * Time.fixedDeltaTime;
            updatedVelocity.x = (Mathf.Abs(xVel) <= Mathf.Epsilon ? 0f : xVel);
        }
        else
        {
            float xVel = updatedVelocity.x + dirX * SidesAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : AirControlMultiplier);
            updatedVelocity.x = Mathf.Sign(xVel) * Mathf.Min(MaxSpeedX, Mathf.Abs(xVel));
        }

        if(dirZ < 0f)
        {
            updatedVelocity.z = Mathf.Max(MinSpeed, updatedVelocity.z + dirZ * BrakeAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : AirControlMultiplier));
            cruiseControl = false;
            cruiseControlSpeed = MinSpeed;
        }
        else if(dirZ == 0f)
        {
            updatedVelocity.z += NeutralAcceleration * Time.fixedDeltaTime;
        }
        else
        {
            updatedVelocity.z = Mathf.Min(MaxSpeedZ, updatedVelocity.z + dirZ * FrontAcceleration * Time.fixedDeltaTime * (isGrounded ? 1f : AirControlMultiplier));
        }

        rb.velocity = updatedVelocity;

        if (rb.velocity.z < cruiseControlSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, cruiseControlSpeed);
        }
    }

}
