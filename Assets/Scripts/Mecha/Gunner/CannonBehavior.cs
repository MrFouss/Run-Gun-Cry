using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform Muzzle;
	public GameObject LaserShot;
    public GameObject MissileShot;
    public float FireRateLaser;
    public float BaseFireRateLaserValue = 0.1f;
    public float FireRateMissile;
    public float BaseFireRateMissileValue = 0.5f;

    public int LaserEnergyConsumption = 2;
    public int MissileEnergyConsumption = 10;

    public float aimRange = Mathf.Infinity;

    private MechaController mechaController;
    private float nextFire;

    private void Awake()
    {
        FireRateLaser = BaseFireRateLaserValue * 1.8f;
        FireRateMissile = BaseFireRateMissileValue * 1.8f;
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

    public void OnSpeedFirePowerBalanceChange(float balanceValue)
    {
        FireRateLaser = (Mathf.Pow(balanceValue * 2.0f + 3.0f, 2.0f) / 5.0f) * BaseFireRateLaserValue;
        FireRateMissile = (Mathf.Pow(balanceValue * 2.0f + 3.0f, 2.0f) / 5.0f) * BaseFireRateMissileValue;
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
