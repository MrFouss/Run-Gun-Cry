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
            EventManager.Instance.OnFirePowerBalanceChange.Invoke(_speedFirePowerBalance / 2.0f + 0.5f);

            mechaController.OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
            canon.OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
            OnSpeedFirePowerBalanceChange(_speedFirePowerBalance);
        }
        get
        {
            return _speedFirePowerBalance;
        }
    }
    private float BaseForwardSpeed = 2f;
    public float BaseSideSpeed = 2f;
    public float ForwardForce = 20f;
    public float SideForce = 50f;
    public float JumpForce = 15f;
    public float AirControlMultiplier = 0.25f;

    private float _maxZeroEnergyForwardSpeed; 
    
    // depend on the situation
    private float _currentMaxForwardSpeed;
    private float _currentMaxSideSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mechaController = GetComponent<MechaController>();
        canon = GetComponent<CannonBehavior>();
        _maxZeroEnergyForwardSpeed = BaseForwardSpeed * 0.75f;
        _currentMaxSideSpeed = BaseSideSpeed;
    }

    private void Start()
    {
        // initialize speed/firepower balance
        SpeedFirePowerBalance = 0;
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
            OnSpeedFirePowerBalanceChange(-1);
        }

        // move forward
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
        if (forwardSpeed.magnitude > _currentMaxForwardSpeed)
        {
            rb.velocity -= forwardSpeed.normalized * (forwardSpeed.magnitude - _currentMaxForwardSpeed);
        }

        // cap side speed
        Vector3 sideSpeed = Vector3.Project(rb.velocity, transform.right);
        if (sideInput == 0)
        {
            // stop side stepping
            rb.velocity -= sideSpeed;
        }
        else if (sideSpeed.magnitude > _currentMaxSideSpeed)
        {
            // remove excess speed
            rb.velocity -= sideSpeed.normalized * (sideSpeed.magnitude - _currentMaxSideSpeed);
        }

        SpeedFirePowerBalance = -1.0f * Input.GetAxis("Vertical");
    }

    private void OnSpeedFirePowerBalanceChange(float balanceValue)
    {
        _currentMaxForwardSpeed = (Mathf.Pow(balanceValue * 2.0f + 3.0f, 2.0f) / 5.0f) * BaseForwardSpeed;
        _currentMaxSideSpeed = (Mathf.Pow(balanceValue * 4.0f + 3.0f, 2.0f) / 5.0f) * BaseSideSpeed;
    }

}
