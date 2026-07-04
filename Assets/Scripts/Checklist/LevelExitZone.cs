using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelExitZone : MonoBehaviour
{
    [SerializeField]
    private bool isEndlessMode = false;

	void OnTriggerEnter2D(Collider2D collision)
	{
        if (!collision.gameObject.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager player))
        {
            return;
        }

        AbstractChecklistSystem checklistSystem = FindObjectOfType<AbstractChecklistSystem>();
        if (checklistSystem.IsChecklistComplete())
        {
            if (isEndlessMode)
            {
                SceneChangeManager.LoadRandomEndlessModeScene();
            } else
            {
                SceneChangeManager sceneChangeManager = FindObjectOfType<SceneChangeManager>();
                sceneChangeManager.LoadNextScene();
            }
        }
	}
}
