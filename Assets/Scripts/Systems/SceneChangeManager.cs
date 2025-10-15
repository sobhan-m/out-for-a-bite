using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public static string GAME_OVER_MENU = "Game Over Menu";
    public static string MAIN_MENU = "Main Menu";

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public String GetActiveSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    
    public void LoadMainMenu()
	{
		LoadScene(MAIN_MENU);
	}
}
