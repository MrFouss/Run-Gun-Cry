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
    private float _speedFirePowerBalance;
    public float SpeedFirePowerBalance
    {
        set
        {
            _speedFirePowerBalance = value;

            EventManager.Instance.OnSpeedBalanceChange.Invoke(_speedFirePowerBalance / 2.0f + 0.5f);
            EventManager.Instance.OnFirePowerBalanceChange.Invoke(1.0f - (_speedFirePowerBalance / 2.0f + 0.5f));

            mechaController.OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
            canon.OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
            OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
        }
        get
        {
            return _speedFirePowerBalance;
        }
    }
    private float BaseSpeedValue = 2;
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
        maxZeroEnergyForwardSpeed = BaseSpeedValue * 0.75f;
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

        SpeedFirePowerBalance = Input.GetAxis("Vertical");
    }

    private void OnSpeedFirePowerBalanceChange(float balanceValue)
    {
        MaxNormalForwardSpeed = (Mathf.Pow(balanceValue * 2.0f + 3.0f, 2.0f) / 5.0f) * BaseSpeedValue;
    }

}
