using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string SCENE_NAME = "SCENE_NAME";
    private const string IS_GARLIC_LOVER_MODE = "IS_GARLIC_LOVER_MODE";
    [SerializeField]
    private bool saveSceneOnLoad = true;
    private SceneChangeManager sceneChangeManager;

	void Awake()
	{
		sceneChangeManager = FindObjectOfType<SceneChangeManager>();
        if (saveSceneOnLoad)
        {
            SaveScene();
        }
	}

	public void SaveScene()
    {
        string sceneName = SceneChangeManager.GetActiveSceneName();
        PlayerPrefs.SetString(SCENE_NAME, sceneName);
    }

    public string GetScene()
    {
        Debug.Log("sceneName = " + PlayerPrefs.GetString(SCENE_NAME));
        return PlayerPrefs.GetString(SCENE_NAME);
    }

    public void LoadScene()
    {
        sceneChangeManager.LoadScene(GetScene());
    }

    public bool HasScene()
    {
        string sceneName = GetScene();
        return sceneName != null && sceneName.Length > 0;
    }

    public void SaveGarlicLoverMode()
    {
        Debug.Log("garlicLoverModeSave = " + SettingSystem.isGarlicLoverMode);
        PlayerPrefs.SetInt(IS_GARLIC_LOVER_MODE, SettingSystem.isGarlicLoverMode ? 1 : 0);
    }

    public bool GetGarlicLoverMode()
    {
        Debug.Log("garlicLoverModeGet = " + PlayerPrefs.GetInt(IS_GARLIC_LOVER_MODE));
        return PlayerPrefs.GetInt(IS_GARLIC_LOVER_MODE) > 0;
    }
}
