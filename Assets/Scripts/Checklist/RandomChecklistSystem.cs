using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RandomChecklistSystem : AbstractChecklistSystem
{
    private const int MAX_INGREDIENTS = 6;
    [SerializeField]
    private List<Transform> spawnLocations;
    [SerializeField]
    private List<GameObject> ingredients;

	void Awake()
	{
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = strikethroughSoundEffect;

        int numOfIngredientsToSpawn = Random.Range(1, MAX_INGREDIENTS + 1);
        spawnLocations.Sort(new RandomSorter()); // Randomize locations used.
        ingredients.Sort(new RandomSorter()); // Randomize ingredients used.
        for (int i = 0; i < numOfIngredientsToSpawn; ++i)
        {
            GameObject.Instantiate(ingredients[i], spawnLocations[i].position, Quaternion.identity);
            ChecklistItem item = ingredients[i].GetComponent<Ingredient>().item;
            checklistItems.Add(item.name, item.isCheckedOff);
        }
	}
}
