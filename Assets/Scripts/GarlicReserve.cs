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
        Debug.Log("GarlicReserve.Awake()");
		if (isEndlessMode)
        {
            DontDestroyOnLoad(this.gameObject);
            GarlicReserve[] garlicReserves = FindObjectsByType<GarlicReserve>(FindObjectsSortMode.None);
            if (garlicReserves.Length > 1)
            {
                Debug.Log("GarlicReserve.Awake() Destroying This");
                Debug.Log("GarlicReserve.Awake() Destroying This " + garlicCount);
                Destroy(this.gameObject);
            }
        } else
        {
            GarlicReserve[] garlicReserves = FindObjectsByType<GarlicReserve>(FindObjectsSortMode.None);
            foreach (GarlicReserve garlicReserve in garlicReserves) {
                if (garlicReserve != this)
                {
                    Destroy(garlicReserve);
                }
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
        Debug.Log("Now Have " + garlicCount + " garlic");
        --garlicCount;
    }
}
