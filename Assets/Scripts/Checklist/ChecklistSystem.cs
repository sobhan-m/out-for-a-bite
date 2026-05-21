using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ChecklistSystem : MonoBehaviour
{
    [SerializeField] private ChecklistItem[] initialChecklist;
    [Min(0)][SerializeField] public float secondsBetweenCharacters;
    public UnityEvent<string> pickupIngredient;
    public Dictionary<string, bool> checklistItems = new Dictionary<string, bool>();

    [Header("Audio")]
    [SerializeField] public AudioClip strikethroughSoundEffect; 
    public AudioSource audioSource {private set; get;}

	void Awake()
	{
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = strikethroughSoundEffect;

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
