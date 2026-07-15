using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessModeCleanupManager : MonoBehaviour
{
	void Awake()
	{
		EndlessScoreTracker[] endlessScoreTrackers = FindObjectsByType<EndlessScoreTracker>(FindObjectsSortMode.None);
        if (SceneChangeManager.GetActiveSceneName() == SceneChangeManager.MAIN_MENU)
        {
            foreach (EndlessScoreTracker scoreTracker in endlessScoreTrackers)
            {
                Destroy(scoreTracker.gameObject);
            }
        }
    }
}
