using UnityEngine;

public class EndlessScoreTracker : MonoBehaviour
{
    private int levelsCompleted;
    public static EndlessScoreTracker instance { private set; get; }
	void Awake()
	{
		if (!SceneChangeManager.IsEndlessMode())
        {
            Debug.Log("is not endless so deleting");
            Destroy(this.gameObject);
        }  else
        {
            Debug.Log("is endless");

            if (instance != null)
            {
                Destroy(this.gameObject);
            } else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
	}

    public static void ClearInstance()
    {
        Destroy(instance);
        instance = null;
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
