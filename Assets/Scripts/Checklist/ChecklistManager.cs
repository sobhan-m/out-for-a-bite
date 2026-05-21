using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;
using System;
using System.Collections;

public class ChecklistManager : MonoBehaviour
{
    private ChecklistSystem checklistSystem;
    private TextMeshProUGUI checklistText;
    private List<string> ingredientNames = new List<string>();
	void Awake()
	{
		checklistText = GetComponent<TextMeshProUGUI>();
        checklistSystem = FindObjectOfType<ChecklistSystem>();
	}

	void Start()
	{
        RegisterIngredientNames();
		UpdateTextWithIngredients();
	}

	public void OnIngredientPickup(string ingredientName)
    {
        StartCoroutine(StrikethroughText(ingredientName));
    }

    private void RegisterIngredientNames()
    {
        ingredientNames.Clear();
        foreach (KeyValuePair<string, bool> item in checklistSystem.checklistItems) {
            string itemName = item.Key;
            bool isCheckedOff = item.Value;
            if (isCheckedOff)
            {
                itemName = RichText.strikethrough(itemName);
            }
            ingredientNames.Add(itemName);
        }
    }

    private void UpdateTextWithIngredients()
    {
        checklistText.text = String.Join("\n", ingredientNames);
    }

    private IEnumerator StrikethroughText(string ingredientName)
    {
        this.checklistSystem.audioSource.Play();
        int indexOfIngredientName = this.ingredientNames.FindIndex(ingredientInList => ingredientInList == ingredientName);
        for (int i = 1; i <= ingredientName.Length; ++i)
        {
            string newIngredientName = RichText.strikethrough(ingredientName.Substring(0, i)) + ingredientName.Substring(i);
            this.ingredientNames[indexOfIngredientName] = newIngredientName;
            UpdateTextWithIngredients();
            yield return new WaitForSeconds(checklistSystem.secondsBetweenCharacters);
        }

        RegisterIngredientNames();
        UpdateTextWithIngredients();

        this.checklistSystem.audioSource.Stop();
    }
}
