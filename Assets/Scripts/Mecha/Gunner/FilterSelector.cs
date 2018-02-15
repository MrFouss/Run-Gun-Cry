using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterSelector : MonoBehaviour {

    private int selectedFilter;
    private int previousFilter;

    public int EnergyConsumption = 1;

    private MechaController mechaController;

    private void Awake()
    {
        mechaController = GetComponent<MechaController>();
    }

    // Use this for initialization
    void Start () {
        selectedFilter = 0;
        previousFilter = 1;
        UpdateFilters();
    }
	
	// Update is called once per frame
	void Update () {
        previousFilter = selectedFilter;
        // Check scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            mechaController.ConsumeEnergy(EnergyConsumption);
            selectedFilter = (selectedFilter + 1) % 3;
            UpdateFilters();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            mechaController.ConsumeEnergy(EnergyConsumption);
            selectedFilter = (selectedFilter + 2) % 3;
            UpdateFilters();
        }
    }

    void UpdateFilters()
    {
        transform.GetChild(0).gameObject.transform.GetChild(selectedFilter).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.transform.GetChild(previousFilter).gameObject.SetActive(false);
    }

    public int GetSelectedFilter()
    {
        return selectedFilter;
    }
}
