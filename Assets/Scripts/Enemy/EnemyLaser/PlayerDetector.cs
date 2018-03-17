using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    #region Private Functions

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.MechaBodyTag)
        {
            GetComponentInParent<EnemyLaserController>().LockNLoad(other.transform);
        }
    }

    #endregion
}
