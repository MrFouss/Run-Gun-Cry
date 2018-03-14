using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{
    private Rigidbody rb;
    private MechaController mechaController;

    // number of grounds currently in collision with the rigid body
    private int grounds = 0;

    // state of the speed/firepower balance
    private int _speedFirePowerBalance;
    public int SpeedFirePowerBalance
    {
        set
        {
            _speedFirePowerBalance = value;

            // TODO Esia
            // EventManager.onSpeedFirePowerBalanceChange.Invoke(_speedFirePowerBalance);
        }
        get
        {
            return _speedFirePowerBalance;
        }
    }
    private float[] maxSpeedValues = new float[5] { 12, 8, 5, 3, 1 };
    
    private float maxForwardSpeed; // depend on the situation
    public float ForwardForce = 20f;
    public float SideForce = 20f;
    public float JumpForce = 25f;
    public float MaxSideSpeed = 25f;
    public float MaxNormalForwardSpeed = 30f;
    public float MaxZeroEnergyForwardSpeed = 20f;
    public float AirControlMultiplier = 0.25f;

    // used to avoid having two consecutive frames reduce speed when holding buttons
    private bool speedIncreasedLastFrame = false;
    private bool speedDecreasedLastFrame = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mechaController = GetComponent<MechaController>();
        // initialize speed/firepower balance
        SpeedFirePowerBalance = 2;

        // TODO Esia
        // EventManager.onSpeedFirePowerBalanceChange.AddListener(OnSpeedFirePowerBalanceChange);
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

        if (!Input.GetButton("IncreaseSpeed"))
        {
            speedIncreasedLastFrame = false;
        }
        else if (!speedIncreasedLastFrame && Input.GetButton("IncreaseSpeed") && SpeedFirePowerBalance > 0)
        {
            SpeedFirePowerBalance--;

            // TODO Esia
            // EventManager.onSpeedFirePowerBalanceChange.Invoke(SpeedFirePowerBalance);
            speedIncreasedLastFrame = true;
        }

        if (!Input.GetButton("IncreaseFirePower"))
        {
            speedDecreasedLastFrame = false;
        }
        else if (!speedDecreasedLastFrame && Input.GetButton("IncreaseFirePower") && SpeedFirePowerBalance < 4)
        {
            SpeedFirePowerBalance++;

            // TODO Esia
            // EventManager.onSpeedFirePowerBalanceChange.Invoke(SpeedFirePowerBalance);
            speedDecreasedLastFrame = true;
        }
    }

    private void OnSpeedFirePowerBalanceChange(int balanceValue)
    {
        MaxNormalForwardSpeed = maxSpeedValues[balanceValue];
    }

}
