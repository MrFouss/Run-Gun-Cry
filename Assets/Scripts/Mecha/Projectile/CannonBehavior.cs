using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform Muzzle;
	public GameObject LaserShot;
    public GameObject MissileShot;
    public float FireRateLaser;
    public float FireRateMissile;

    private float nextFire;
    
	void Update () 
	{
		
		if (Input.GetButtonDown("FireLaser") && Time.time > nextFire)
		{
            nextFire = Time.time + FireRateLaser;
			Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
		}

        if (Input.GetButtonDown("FireMissile") && Time.time > nextFire)
        {
            nextFire = Time.time + FireRateMissile;
            Instantiate(MissileShot, Muzzle.position, Muzzle.rotation);
        }

    }	
}
