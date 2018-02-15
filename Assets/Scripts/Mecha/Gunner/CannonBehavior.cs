using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform Muzzle;
	public GameObject LaserShot;
    public GameObject MissileShot;
    public float FireRateLaser = 0.1f;
    public float FireRateMissile = 1.0f;

    public int LaserEnergyConsumption = 1;
    public int MissileEnergyConsumption = 10;

    private MechaController mechaConstroller;

    private float nextFire;

    private void Awake()
    {
        mechaConstroller = GetComponent<MechaController>();
    }

    void Update () 
	{
		
		if (mechaConstroller.CanConsumeEnergy(LaserEnergyConsumption) && Input.GetButton("FireLaser") && Time.time > nextFire)
		{
            nextFire = Time.time + FireRateLaser;
			Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
            mechaConstroller.ConsumeEnergy(LaserEnergyConsumption);
		}

        if (mechaConstroller.CanConsumeEnergy(MissileEnergyConsumption) && Input.GetButton("FireMissile") && Time.time > nextFire)
        {
            nextFire = Time.time + FireRateMissile;
            Instantiate(MissileShot, Muzzle.position, Muzzle.rotation);
            mechaConstroller.ConsumeEnergy(MissileEnergyConsumption);
        }

    }	
}
