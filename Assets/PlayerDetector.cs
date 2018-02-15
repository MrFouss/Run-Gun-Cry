using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MechaBody")
        {
            GetComponentInParent<EnemyLaserBehavior>().LockNLoad(other.transform);
        }
    }
}
