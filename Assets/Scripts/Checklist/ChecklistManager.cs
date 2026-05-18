using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class ChecklistManager : MonoBehaviour
{
    private ChecklistSystem checklistSystem;
    private TextMeshProUGUI checklistText;
	void Awake()
	{
		checklistText = GetComponent<TextMeshProUGUI>();
        checklistSystem = FindObjectOfType<ChecklistSystem>();
	}

	void Start()
	{
		UpdateTextWithIngredients();
	}

	public void OnIngredientPickup()
    {
        UpdateTextWithIngredients();
    }

    private void UpdateTextWithIngredients()
    {
        StringBuilder outputString = new StringBuilder();
        Debug.Log("han" + checklistSystem.checklistItems.Count);
        foreach (KeyValuePair<string, bool> item in checklistSystem.checklistItems) {
            string itemName = item.Key;
            bool isCheckedOff = item.Value;
            if (isCheckedOff)
            {
                itemName = RichText.strikethrough(itemName);
            }
            outputString.Append(itemName + "\n");
        }
        checklistText.text = outputString.ToString();
    }
}
