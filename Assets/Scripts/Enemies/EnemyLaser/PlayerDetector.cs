using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    #region Private Functions

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MechaBody")
        {
            GetComponentInParent<EnemyLaserBehavior>().LockNLoad(other.transform);
        }
    }

    #endregion
}
