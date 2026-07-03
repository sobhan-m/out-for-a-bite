using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Transform> spawnLocations;
    [SerializeField]
    private int minCount;
    [SerializeField]
    private int maxCount;

	void Awake()
	{
		int prefabsToSpawn = Random.Range(minCount, maxCount + 1);
        spawnLocations.Sort(new RandomSorter());

        for (int i = 0; i < prefabsToSpawn; ++i)
        {
            Instantiate(prefab, spawnLocations[i].position, Quaternion.identity);
        }
	}
}
