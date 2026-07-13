using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChecklistSystem : AbstractChecklistSystem
{
    [SerializeField] private ChecklistItem[] initialChecklist;

	void Awake()
	{
        if (!SceneChangeManager.IsCombatScene())
        {
            return;
        }
        
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = strikethroughSoundEffect;

		foreach (ChecklistItem item in initialChecklist)
        {
            checklistItems.Add(item.name, item.isCheckedOff);
        }
	}
}
