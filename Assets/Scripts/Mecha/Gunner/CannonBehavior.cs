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

    private MechaController mechaController;

    private float nextFire;

    private void Awake()
    {
        mechaController = GetComponent<MechaController>();
    }

    void Update () 
	{
		if (mechaController.CanConsumeEnergy(LaserEnergyConsumption) && Input.GetButton("FireLaser") && Time.time > nextFire)
		{
            nextFire = Time.time + FireRateLaser;
            EventManager.onGunnerShot.Invoke(ShotType.Laser);
            // for the scoring script
            EventManager.onGunnerConsumesEnergy.Invoke(LaserEnergyConsumption);
			Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
            mechaController.ConsumeEnergy(LaserEnergyConsumption);
		}

        if (mechaController.CanConsumeEnergy(MissileEnergyConsumption) && Input.GetButton("FireMissile") && Time.time > nextFire)
        {
            nextFire = Time.time + FireRateMissile;
            EventManager.onGunnerShot.Invoke(ShotType.Missile);
            // for the scoring script
            EventManager.onGunnerConsumesEnergy.Invoke(MissileEnergyConsumption);
            Instantiate(MissileShot, Muzzle.position, Muzzle.rotation);
            mechaController.ConsumeEnergy(MissileEnergyConsumption);
        }
    }	
}
