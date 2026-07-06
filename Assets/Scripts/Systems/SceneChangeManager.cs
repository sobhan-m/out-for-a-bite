using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public static string GAME_OVER_MENU = "Game Over Menu";
    public static string MAIN_MENU = "Main Menu";
    public static string NEW_GAME_SCENE = "Outside - 1";

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadNewGameScene()
    {
        LoadScene(NEW_GAME_SCENE);
    }
    
    public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public static String GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    
    public void LoadMainMenu()
	{
		LoadScene(MAIN_MENU);
	}

    public static bool IsCombatScene()
    {
        return GetActiveSceneName().Contains("Outside");
    }

    public static bool IsEndlessMode()
    {
        string currentSceneName = SceneChangeManager.GetActiveSceneName();
        return currentSceneName.Contains("Endless") || currentSceneName == SceneChangeManager.GAME_OVER_MENU;
    }

    public static void LoadRandomEndlessModeScene()
    {
        const int MIN_ENDLESS_MAP = 1;
        const int MAX_ENDLESS_MAP = 7;
        int i = UnityEngine.Random.Range(MIN_ENDLESS_MAP, MAX_ENDLESS_MAP + 1);
        string sceneToLoad = "Outside - Endless - " + i;
        while (sceneToLoad == GetActiveSceneName())
        {
            i = UnityEngine.Random.Range(MIN_ENDLESS_MAP, MAX_ENDLESS_MAP + 1);
            sceneToLoad = "Outside - Endless - " + i; 
        }
        if (EndlessScoreTracker.instance != null)
        {
            if (IsEndlessMode())
            {
                EndlessScoreTracker.instance.IncreaseLevelsCompleted();
            } else
            {
                EndlessScoreTracker.ClearInstance();
            }
        }
        if (EndlessScoreTracker.instance != null && !IsEndlessMode())
        {
            EndlessScoreTracker.instance.IncreaseLevelsCompleted();
        }
        LoadScene(sceneToLoad);
    }
}
