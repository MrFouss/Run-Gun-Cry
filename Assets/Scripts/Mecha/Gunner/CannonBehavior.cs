using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform Muzzle;
	public GameObject LaserShot;
    public GameObject MissileShot;
    public float FireRateLaser;
    public float[] FireRateLaserValues = new float[5] { 0.7f, 0.4f, 0.2f, 0.1f, 0.05f};
    public float FireRateMissile;
    public float[] FireRateMissileValues = new float[5] { 2f, 1.5f, 1f, 0.7f, 0.4f };

    public int LaserEnergyConsumption = 1;
    public int MissileEnergyConsumption = 10;

    private MechaController mechaController;

    private float nextFire;

    private void Awake()
    {
        FireRateLaser = FireRateLaserValues[2];
        FireRateMissile = FireRateMissileValues[2];
        mechaController = GetComponent<MechaController>();
        EventManager.onSpeedFirePowerBalanceChange.AddListener(OnSpeedFirePowerBalanceChange);
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

    private void OnSpeedFirePowerBalanceChange(int balanceValue)
    {
        FireRateLaser = FireRateLaserValues[balanceValue];
        FireRateMissile = FireRateMissileValues[balanceValue];
    }
}
