using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    private bool saveSceneOnLoad = true;
    private const string SCENE_NAME = "SCENE_NAME";
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
        string sceneName = sceneChangeManager.GetActiveSceneName();
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
}
