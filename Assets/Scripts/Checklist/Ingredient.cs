using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Ingredient : MonoBehaviour, IPickupable
{
    [SerializeField] public ChecklistItem item;
    private AbstractChecklistSystem checklistSystem;

	void Awake()
	{
		checklistSystem = FindObjectOfType<AbstractChecklistSystem>();
	}

	public void PickUp()
	{
		checklistSystem.RegisterPickedUpItem(item);
        Destroy(gameObject);
	}
}
