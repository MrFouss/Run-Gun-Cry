using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{
    private Rigidbody rb;
    private MechaController mechaController;

    private CannonBehavior canon;

    // number of grounds currently in collision with the rigid body
    private int grounds = 0;

    // state of the speed/firepower balance
    private int _speedFirePowerBalance;
    public int SpeedFirePowerBalance
    {
        set
        {
            _speedFirePowerBalance = Mathf.Clamp(value, 0, 4);

            EventManager.Instance.OnSpeedBalanceChange.Invoke((4.0f - _speedFirePowerBalance) / 4.0f);
            EventManager.Instance.OnFirePowerBalanceChange.Invoke(_speedFirePowerBalance / 4.0f);

            mechaController.OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
            canon.OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
            OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
        }
        get
        {
            return _speedFirePowerBalance;
        }
    }
    private float[] maxSpeedValues = new float[5] { 12, 8, 5, 3, 1 };
    private float maxZeroEnergyForwardSpeed;

    private float maxForwardSpeed; // depend on the situation
    public float ForwardForce = 20f;
    public float SideForce = 20f;
    public float JumpForce = 25f;
    public float MaxSideSpeed = 25f;
    public float MaxNormalForwardSpeed = 30f;
    public float AirControlMultiplier = 0.25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mechaController = GetComponent<MechaController>();
        canon = GetComponent<CannonBehavior>();
        maxZeroEnergyForwardSpeed = maxSpeedValues[4];
    }

    private void Start()
    {
        // initialize speed/firepower balance
        SpeedFirePowerBalance = 2;
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
            maxForwardSpeed = maxZeroEnergyForwardSpeed;
        }
        else
        {
            maxForwardSpeed = MaxNormalForwardSpeed;
        }

        // move forward
        // TODO fix to avoid stoping on the side of platforms
        rb.AddRelativeForce(Vector3.forward * ForwardForce);
        
        // jump
        if (grounds > 0 && Input.GetButtonDown("Jump"))
        {
            rb.AddRelativeForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        //Return to ground
        if (grounds == 0 && Input.GetButtonDown("ReturnToGround"))
        {
            rb.AddRelativeForce(Vector3.down * 10, ForceMode.Impulse);
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

        if (Input.GetButtonDown("IncreaseSpeed"))
        {
            SpeedFirePowerBalance--;
        }

        if (Input.GetButtonDown("IncreaseFirePower"))
        {
            SpeedFirePowerBalance++;
        }
    }

    private void OnSpeedFirePowerBalanceChange(int balanceValue)
    {
        MaxNormalForwardSpeed = maxSpeedValues[balanceValue];
    }

}
