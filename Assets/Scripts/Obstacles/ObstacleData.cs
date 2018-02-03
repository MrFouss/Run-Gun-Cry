using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour {

    // Data about an obstacle
    public int structurePoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Destroys the obstacles
    public void DestroyObstacle() {
        Destroy(gameObject);
    }
}
