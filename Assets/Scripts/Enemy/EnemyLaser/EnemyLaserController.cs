using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserController : MonoBehaviour
{
    #region Public Functions

    public void LockNLoad(Transform playerTransform)
    {
        transform.LookAt(playerTransform);
        GetComponentInChildren<EnemyCannonBehavior>().Fire();
    }

    #endregion
}
