using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour {

    public Transform PlayerTransform;
    public GameObject[] EnvironmentPrefabPool;
    public GameObject[] PreInstantiatedEnvironmentPrefabs;
    private List<GameObject> InstantiatedEnvironmentPrefabs = new List<GameObject>();
    public float DistanceToDisappearingObject = 20.0f;
    public float PrefabHorizontalSpacing = 10.0f;

    // Use this for initialization
    void Start () {
		for (int i = 0; i < PreInstantiatedEnvironmentPrefabs.Length; i++) {
            InstantiatedEnvironmentPrefabs.Add(PreInstantiatedEnvironmentPrefabs[i]);
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
            InstantiatedEnvironmentPrefabs.Add(Instantiate(chosenPrefab, instantiationPosition, new Quaternion()));
        }
	}
}
