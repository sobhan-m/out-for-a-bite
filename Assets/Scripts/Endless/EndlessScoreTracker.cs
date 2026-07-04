using UnityEngine;

public class EndlessScoreTracker : MonoBehaviour
{
    private int levelsCompleted;
	void Awake()
	{
        string currentSceneName = SceneChangeManager.GetActiveSceneName();
        bool isEndless = currentSceneName.Contains("Endless") || currentSceneName == SceneChangeManager.GAME_OVER_MENU;
		if (!isEndless)
        {
            Debug.Log("is not endless so deleting");
            Destroy(this.gameObject);
        }  else
        {
            Debug.Log("is endless");
            DontDestroyOnLoad(this.gameObject);
            EndlessScoreTracker[] trackers = FindObjectsByType<EndlessScoreTracker>(FindObjectsSortMode.None);
            if (trackers.Length > 1)
            {
                Debug.Log("deleting this + " + levelsCompleted);
                Destroy(this.gameObject);
            }
        }
	}

    public void IncreaseLevelsCompleted()
    {
        Debug.Log("incrementing levels completed");
        levelsCompleted++;
    }

    public int GetLevelsCompleted()
    {
        return levelsCompleted;
    }
}
