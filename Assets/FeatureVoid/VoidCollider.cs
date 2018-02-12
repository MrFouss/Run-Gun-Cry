using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidCollider : MonoBehaviour {

    private int heightRespawn = 0;
    // This script repositions the mecha whenever he falls into the void
    // A PlatforMemorizer Script must be attached to the mecha beforehand

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.tag == "MechaBody")
        {
            // When the Mecha falls, reposition it over the last platform it landed on
            GameObject lastPlatform = other.GetComponent<MechaController>().LastPlatform;
            other.transform.position = new Vector3(lastPlatform.transform.position.x, lastPlatform.transform.position.y + heightRespawn, lastPlatform.transform.position.z);
        }
    }
    
}
