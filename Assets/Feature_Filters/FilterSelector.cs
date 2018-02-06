using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterSelector : MonoBehaviour {

    private int SelectedFilter;
    private int PreviousFilter;

	// Use this for initialization
	void Start () {
        SelectedFilter = 0;
        PreviousFilter = 1;
        UpdateFilters();
    }
	
	// Update is called once per frame
	void Update () {
        PreviousFilter = SelectedFilter;
        // Check scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SelectedFilter = (SelectedFilter + 1) % 3;
            UpdateFilters();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            SelectedFilter = (SelectedFilter + 2) % 3;
            UpdateFilters();
        }
    }

    void UpdateFilters()
    {
        transform.GetChild(0).gameObject.transform.GetChild(SelectedFilter).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.transform.GetChild(PreviousFilter).gameObject.SetActive(false);
    }

    public int GetSelectedFilter()
    {
        return SelectedFilter;
    }
}
