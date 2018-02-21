using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform Muzzle;
	public GameObject LaserShot;
    public GameObject MissileShot;
    public float FireRateLaser = 0.1f;
    public float FireRateMissile = 1.0f;

    private float nextFire;
    
	void Update () 
	{
		
		if (Input.GetButton("FireLaser") && Time.time > nextFire)
		{
            nextFire = Time.time + FireRateLaser;
			Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
		}

        if (Input.GetButton("FireMissile") && Time.time > nextFire)
        {
            nextFire = Time.time + FireRateMissile;
            Instantiate(MissileShot, Muzzle.position, Muzzle.rotation);
        }

    }	
}
