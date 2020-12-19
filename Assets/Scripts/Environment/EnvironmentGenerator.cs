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
            Vector3 instantiationPosition = InstantiatedEnvironmentPrefabs[InstantiatedEnvironmentPrefabs.Count - 1].transform.position;
            instantiationPosition += Vector3.forward * PrefabHorizontalSpacing;
            SpawnEnvironmentPiece(instantiationPosition);
            SpawnEnemy(instantiationPosition);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if ((PlayerTransform.position - InstantiatedEnvironmentPrefabs[0].transform.position).z > DistanceToDisappearingObject)
        {
            Destroy(InstantiatedEnvironmentPrefabs[0]);
            InstantiatedEnvironmentPrefabs.RemoveAt(0);

            Vector3 instantiationPosition = InstantiatedEnvironmentPrefabs[InstantiatedEnvironmentPrefabs.Count - 1].transform.position;
            instantiationPosition += Vector3.forward * PrefabHorizontalSpacing;
            SpawnEnvironmentPiece(instantiationPosition);
            SpawnEnemy(instantiationPosition);
        }
    }

    private void SpawnEnvironmentPiece(Vector3 position)
    {
        GameObject chosenPrefab = EnvironmentPrefabPool[Random.Range(0, EnvironmentPrefabPool.Length)];
        InstantiatedEnvironmentPrefabs.Add(Instantiate(chosenPrefab, position, Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f))));
    }

    private void SpawnEnemy(Vector3 position)
    {
        Instantiate(EnemiesPool[Random.Range(0, EnemiesPool.Length)], position, Quaternion.LookRotation(PlayerTransform.position - position, PlayerTransform.up));
        Instantiate(EnemiesPool[Random.Range(0, EnemiesPool.Length)], position + (Vector3.forward * PrefabHorizontalSpacing / 2f), Quaternion.LookRotation(PlayerTransform.position - position, PlayerTransform.up));
    }
}
