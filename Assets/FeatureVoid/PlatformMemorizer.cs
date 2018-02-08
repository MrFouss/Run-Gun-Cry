using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMemorizer : MonoBehaviour {

    // This script must be attached to the mecha
    // in order to keep track of the last platform
    // it landed on
    // This feature is used to reposition the mecha
    // after falling into the void

    private GameObject lastPlatForm;

	// Use this for initialization
	void Start () {
        // Use a random platform for initialization
        lastPlatForm = GameObject.FindGameObjectWithTag("Platform");
	}

    private void OnCollisionEnter(Collision collision)
    {
        // On collision with a platform, the latter is memorized
        if (collision.collider.gameObject.tag == "Platform")
        {
            lastPlatForm = collision.collider.gameObject;
        }
    }

    public GameObject getLastPlatform()
    {
        return lastPlatForm;
    }



}
