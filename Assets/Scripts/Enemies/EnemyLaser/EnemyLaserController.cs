﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserController : MonoBehaviour
{
    #region Public Functions

    public void LockNLoad(Transform PlayerTransform)
    {
        this.transform.LookAt(PlayerTransform);
        this.GetComponentInChildren<EnemyCannonBehavior>().Fire();
    }

    #endregion
}