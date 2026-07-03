using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicReserve : MonoBehaviour
{
    [SerializeField] public int garlicCount = 3;
    [SerializeField] 
    private bool isEndlessMode = false;

	void Awake()
	{
		if (isEndlessMode)
        {
            DontDestroyOnLoad(this.gameObject);
            GarlicReserve[] garlicReserves = FindObjectsByType<GarlicReserve>(FindObjectsSortMode.None);
            if (garlicReserves.Length > 1)
            {
                Destroy(this);
            }
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
        --garlicCount;
    }
}
