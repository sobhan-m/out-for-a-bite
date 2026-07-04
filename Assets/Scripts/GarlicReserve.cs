using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicReserve : MonoBehaviour
{
    [SerializeField] public int garlicCount = 3;
    [SerializeField] 
    private bool isEndlessMode = false;

    public static GarlicReserve instance {private set; get;}

	void Awake()
	{
		if (isEndlessMode)
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            } else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        } else
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
            instance = this;
        }
	}

	void Start()
	{
        SettingSystem settingSystem = FindObjectOfType<SettingSystem>();
		if (settingSystem != null && SettingSystem.isGarlicLoverMode)
        {
            garlicCount = SettingSystem.GARLIC_LOVER_MODE_COUNT;
        }
	}

	public bool HasGarlic()
    {
        return garlicCount > 0;
    }

    public void UseGarlic()
    {
        Debug.Log("Now Have " + garlicCount + " garlic");
        --garlicCount;
    }
}
