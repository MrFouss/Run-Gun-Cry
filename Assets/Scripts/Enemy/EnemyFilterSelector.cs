using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFilterSelector : MonoBehaviour {

    // This script can be attached to enemies who require a special filter to be seen
    // Attach it and a random filter will be attributed on Awake
    // The EnemyVisualizer will take care of displaying the right ones
    private FilterColor requiredFilter;

	void Start() {
        requiredFilter = (FilterColor) Random.Range(0, 3);
        EventManager.Instance.OnFilterSelected.AddListener(Display);
	}
	
	// Update is called once per frame
	void Display (FilterColor filterSelected) {
        if (filterSelected != requiredFilter)
        {
            transform.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            transform.GetComponent<Renderer>().enabled = true;
        }
	}

}
