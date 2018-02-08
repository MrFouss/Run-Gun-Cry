using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidMechaFollower : MonoBehaviour {

    public GameObject mecha;
	
	// Update is called once per frame
	void Update () {
        // The void collider stays under the mecha and follows the player
        // along the z and x axis.
        transform.position = new Vector3(mecha.transform.position.x, transform.position.y, mecha.transform.position.z);
	}

}
