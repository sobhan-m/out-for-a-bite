using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelExitZone : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
        if (!collision.gameObject.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager player))
        {
            return;
        }

        ChecklistSystem checklistSystem = FindObjectOfType<ChecklistSystem>();
        if (checklistSystem.IsChecklistComplete())
        {
            SceneChangeManager sceneChangeManager = FindObjectOfType<SceneChangeManager>();
            sceneChangeManager.LoadNextScene();
        }
	}
}
