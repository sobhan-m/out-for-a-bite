using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicReserve : MonoBehaviour
{
    [SerializeField] public int garlicCount = 3;

	void Start()
	{
        SettingSystem settingSystem = FindObjectOfType<SettingSystem>();
		if (settingSystem != null && settingSystem.isGarlicLoverMode)
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
