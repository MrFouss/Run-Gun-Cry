﻿using UnityEngine;
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

    public float aimRange = Mathf.Infinity;

    private MechaController mechaController;
    private float nextFire;

    private void Awake()
    {
        FireRateLaser = FireRateLaserValues[2];
        FireRateMissile = FireRateMissileValues[2];
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
			GameObject projectile = Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
            Physics.IgnoreCollision(projectile.GetComponentInChildren<Collider>(), GetComponent<Collider>());
            mechaController.ConsumeEnergy(LaserEnergyConsumption);
            AimToTarget(projectile);
		}

        if (mechaController.CanConsumeEnergy(MissileEnergyConsumption) && Input.GetButton("FireMissile") && Time.time > nextFire)
        {
            nextFire = Time.time + FireRateMissile;
            EventManager.onGunnerShot.Invoke(ShotType.Missile);
            // for the scoring script
            EventManager.onGunnerConsumesEnergy.Invoke(MissileEnergyConsumption);
            GameObject projectile = Instantiate(MissileShot, Muzzle.position, Muzzle.rotation);
            Physics.IgnoreCollision(projectile.GetComponentInChildren<Collider>(), GetComponent<Collider>());
            mechaController.ConsumeEnergy(MissileEnergyConsumption);
            AimToTarget(projectile);
        }
    }	

    public void OnSpeedFirePowerBalanceChange(int balanceValue)
    {
        FireRateLaser = FireRateLaserValues[balanceValue];
        FireRateMissile = FireRateMissileValues[balanceValue];
    }

    private Ray GetRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void AimToTarget(GameObject projectile)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(GetRay(), out hitInfo, aimRange))
        {
            Vector3 transformToAim = hitInfo.point;
            projectile.transform.LookAt(transformToAim);
        }
        else
        {
            // projectile.transform.LookAt(GetRay().direction);
        }
    }
}
