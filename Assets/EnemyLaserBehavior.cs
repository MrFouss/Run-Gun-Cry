using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour {

    void Start ()
    {
        
    }

    void Update ()
    {
           
	}

    public void LockNLoad(Transform PlayerTransform)
    {
        this.transform.LookAt(PlayerTransform);
        this.GetComponentInChildren<EnemyCannonBehavior>().Fire();

    }

    
}
