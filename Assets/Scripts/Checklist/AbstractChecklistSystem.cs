using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public abstract class AbstractChecklistSystem : MonoBehaviour
{
    [Min(0)][SerializeField] public float secondsBetweenCharacters;
    public UnityEvent<string> pickupIngredient;
    public Dictionary<string, bool> checklistItems = new Dictionary<string, bool>();

    [Header("Audio")]
    [SerializeField] public AudioClip strikethroughSoundEffect; 
    public AudioSource audioSource {protected set; get;}

    public void RegisterPickedUpItem(ChecklistItem itemPickedUp)
    {
        if (checklistItems.ContainsKey(itemPickedUp.name))
        {
            checklistItems[itemPickedUp.name] = true;
            pickupIngredient.Invoke(itemPickedUp.name);
        }
    }

    public bool IsChecklistComplete()
    {
        foreach (bool isIngredientCheckedOff in checklistItems.Values)
        {
            if (!isIngredientCheckedOff)
            {
                return false;
            }
        }
        return true;
    }
}
