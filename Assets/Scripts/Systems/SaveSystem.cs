using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string SCENE_NAME = "SCENE_NAME";
    private const string IS_GARLIC_LOVER_MODE = "IS_GARLIC_LOVER_MODE";
    [SerializeField]
    private bool saveSceneOnLoad = true;
    private SceneChangeManager sceneChangeManager;
    private SettingSystem settingSystem;

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
        string sceneName = sceneChangeManager.GetActiveSceneName();
        PlayerPrefs.SetString(Save.SCENE_NAME, sceneName);
    }

    public string GetScene()
    {
        Debug.Log("sceneName = " + PlayerPrefs.GetString(Save.SCENE_NAME));
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
        PlayerPrefs.SetInt(Save.IS_GARLIC_LOVER_MODE, settingSystem.isGarlicLoverMode ? 1 : 0);
    }

    public bool GetGarlicLoverMode()
    {
        return PlayerPrefs.GetInt(Save.IS_GARLIC_LOVER_MODE) > 0;
    }
}
