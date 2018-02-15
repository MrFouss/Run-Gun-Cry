using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBehavior : MonoBehaviour {

    public Transform Muzzle;
    public GameObject LaserShot;
    public float FireRateLaser = 0.1f;

    private float nextFire;

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRateLaser;
            Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
        }
        
    }
}
