using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public Transform Muzzle;
	public GameObject LaserShot;
    public GameObject MissileShot;
    public float FireRateLaser;
    public float FireRateMissile;
    private float NextFire;
    
	void Update () 
	{
		
		if (Input.GetKey("mouse 0") && Time.time > NextFire)
		{
            NextFire = Time.time + FireRateLaser;
			GameObject Go = GameObject.Instantiate(LaserShot, Muzzle.position, Muzzle.rotation) as GameObject;
		}

        if (Input.GetKey("mouse 1") && Time.time > NextFire)
        {
            NextFire = Time.time + FireRateMissile;
            GameObject Go = GameObject.Instantiate(MissileShot, Muzzle.position, Muzzle.rotation) as GameObject;
        }

    }	
}
