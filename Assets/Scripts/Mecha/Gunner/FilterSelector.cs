using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterSelector : MonoBehaviour {

    public enum FilterColor { RED=0, GREEN=1, BLUE=2 };

    private FilterColor _selectedFilter;
    public FilterColor SelectedFilter
    {
        set
        {
            _selectedFilter = value;
            EventManager.onFilterSelected.Invoke(_selectedFilter);
        }
        get
        {
            return _selectedFilter;
        }
    }

    public int EnergyConsumption = 1;

    private MechaController mechaController;

    private void Awake()
    {
        mechaController = GetComponent<MechaController>();
    }

    // Use this for initialization
    void Start () {
        SelectedFilter = FilterColor.RED;
    }
	
	// Update is called once per frame
	void Update () {
        // Check scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            mechaController.ConsumeEnergy(EnergyConsumption);
            SelectedFilter = (FilterColor) ( ((int)SelectedFilter + 1) % 3);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            mechaController.ConsumeEnergy(EnergyConsumption);
            SelectedFilter = (FilterColor)(((int)SelectedFilter + 2) % 3);
        }
    }
}
