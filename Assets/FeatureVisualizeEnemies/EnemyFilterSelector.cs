using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFilterSelector : MonoBehaviour {

    // This script can be attached to enemies who require a special filter to be seen
    // Attach it and a random filter will be attributed on Awake
    // The EnemyVisualizer will take care of displaying the right ones
    public int RequiredFilter;

	void Awake() {
        RequiredFilter = Random.Range(0, 3);
        EventManager.onFilterSelected.AddListener(Display);
	}
	
	// Update is called once per frame
	void Display (int filterSelected) {
        if (filterSelected != RequiredFilter)
        {
            transform.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            transform.GetComponent<Renderer>().enabled = true;
        }
	}

}
