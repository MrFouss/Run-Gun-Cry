﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBehavior : MonoBehaviour
{

    #region Public Variables

    public Transform Muzzle;
    public GameObject LaserShot;
    public float FireRateLaser = 0.1f;

    #endregion

    #region Private Variables

    private float nextFire;

    #endregion

    #region Public Functions

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRateLaser;
            Instantiate(LaserShot, Muzzle.position, Muzzle.rotation);
        }
        
    }

    #endregion
}
