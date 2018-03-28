using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour {

    public Transform PlayerTransform;
    public GameObject[] EnvironmentPrefabPool;
    public GameObject[] EnemiesPool;

    public int InitialNumberOfElements = 10;
    public float DistanceToDisappearingObject = 20.0f;
    public float PrefabHorizontalSpacing = 10.0f;

    private List<GameObject> InstantiatedEnvironmentPrefabs = new List<GameObject>();

    // Use this for initialization
    void Start () {
        InstantiatedEnvironmentPrefabs.Add(Instantiate(EnvironmentPrefabPool[0], Vector3.zero, new Quaternion()));

        for (int i = 1; i < InitialNumberOfElements; i++) {
            GameObject chosenPrefab = EnvironmentPrefabPool[Random.Range(0, EnvironmentPrefabPool.Length)];
            Vector3 instantiationPosition = InstantiatedEnvironmentPrefabs[InstantiatedEnvironmentPrefabs.Count - 1].transform.position;
            instantiationPosition += (Vector3.forward * PrefabHorizontalSpacing);
            InstantiatedEnvironmentPrefabs.Add(Instantiate(chosenPrefab, instantiationPosition, Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f))));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if ((PlayerTransform.position - InstantiatedEnvironmentPrefabs[0].transform.position).z > DistanceToDisappearingObject) {
            Destroy(InstantiatedEnvironmentPrefabs[0]);
            InstantiatedEnvironmentPrefabs.RemoveAt(0);

            GameObject chosenPrefab = EnvironmentPrefabPool[Random.Range(0, EnvironmentPrefabPool.Length)];
            Vector3 instantiationPosition = InstantiatedEnvironmentPrefabs[InstantiatedEnvironmentPrefabs.Count - 1].transform.position;
            instantiationPosition += (Vector3.forward * PrefabHorizontalSpacing);
            InstantiatedEnvironmentPrefabs.Add(Instantiate(chosenPrefab, instantiationPosition, Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f))));

            if (Random.Range(0, 2) == 0)
            {
                Instantiate(EnemiesPool[Random.Range(0, EnemiesPool.Length)], instantiationPosition, new Quaternion());
            }
        }
	}
}
