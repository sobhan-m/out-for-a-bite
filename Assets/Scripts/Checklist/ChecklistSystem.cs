using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChecklistSystem : MonoBehaviour
{
    [SerializeField] private ChecklistItem[] initialChecklist;
    [Min(0)][SerializeField] public float secondsBetweenCharacters;
    public UnityEvent<string> pickupIngredient;
    public Dictionary<string, bool> checklistItems = new Dictionary<string, bool>();

	void Awake()
	{
		foreach (ChecklistItem item in initialChecklist)
        {
            checklistItems.Add(item.name, item.isCheckedOff);
        }
	}


	public void RegisterPickedUpItem(ChecklistItem itemPickedUp)
    {
        if (checklistItems.ContainsKey(itemPickedUp.name))
        {
            checklistItems[itemPickedUp.name] = true;
            pickupIngredient.Invoke(itemPickedUp.name);
        }
    }
}
