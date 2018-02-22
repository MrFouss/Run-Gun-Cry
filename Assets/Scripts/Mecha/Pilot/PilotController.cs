using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{
    private Rigidbody rb;
    private MechaController mechaController;

    // number of grounds currently in collision with the rigid body
    private int grounds = 0;
    
    private float maxForwardSpeed; // depend on the situation
    public float ForwardForce = 20f;
    public float SideForce = 20f;
    public float JumpForce = 25f;
    public float MaxSideSpeed = 25f;
    public float MaxNormalForwardSpeed = 30f;
    public float MaxZeroEnergyForwardSpeed = 20f;
    public float AirControlMultiplier = 0.25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mechaController = GetComponent<MechaController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounds++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounds--;
        }
    }

    private void FixedUpdate()
    {
        // change max speed depending on the energy
        if (mechaController.Energy == 0)
        {
            maxForwardSpeed = MaxZeroEnergyForwardSpeed;
        }
        else
        {
            maxForwardSpeed = MaxNormalForwardSpeed;
        }

        // move forward
        rb.AddRelativeForce(Vector3.forward * ForwardForce);

        
        // jump
        if (grounds > 0 && Input.GetButtonDown("Jump"))
        {
            rb.AddRelativeForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        // move left and right
        float sideInput = Input.GetAxis("Horizontal");
        rb.AddRelativeForce(Vector3.right * SideForce * sideInput * (grounds > 0 ? 1f : AirControlMultiplier));

        // cap forward speed
        Vector3 forwardSpeed = Vector3.Project(rb.velocity, transform.forward);
        if (forwardSpeed.magnitude > maxForwardSpeed)
        {
            rb.velocity -= forwardSpeed.normalized * (forwardSpeed.magnitude - maxForwardSpeed);
        }

        // cap side speed
        Vector3 sideSpeed = Vector3.Project(rb.velocity, transform.right);
        if (sideSpeed.magnitude > MaxSideSpeed)
        {
            rb.velocity -= sideSpeed.normalized * (sideSpeed.magnitude - MaxSideSpeed);
        }
    }

}
