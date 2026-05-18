using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Ingredient : MonoBehaviour, IPickupable
{
    [SerializeField] private ChecklistItem item;
    private ChecklistSystem checklistSystem;

	void Awake()
	{
		checklistSystem = FindObjectOfType<ChecklistSystem>();
	}

	public void PickUp()
	{
		checklistSystem.RegisterPickedUpItem(item);
        Destroy(gameObject);
	}
}
