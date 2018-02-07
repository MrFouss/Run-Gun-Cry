using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeEnemies : MonoBehaviour {

    private GameObject[] enemiesList;
    public GameObject filtersSystem;
    private FilterSelector filterSelector;

	// Use this for initialization
	void Start () {
        filterSelector = filtersSystem.GetComponent<FilterSelector>();
        updateEnemiesList();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject enemy in enemiesList)
        {
            if (filterSelector.GetSelectedFilter() == enemy.GetComponent<EnemyFilterSelector>().RequiredFilter)
            {
                enemy.GetComponent<Renderer>().enabled = true;
            } else
            {
                enemy.GetComponent<Renderer>().enabled = false;
            }
        }
	}

    // To be called whenever an enemy is created for it to be analyzed later on by the visualizer
    public void updateEnemiesList()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
