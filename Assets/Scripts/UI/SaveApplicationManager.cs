using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveApplicationManager : MonoBehaviour
{
	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private Toggle garlicLoverModeToggle;

    private SaveSystem saveSystem;
    private SettingSystem settingSystem; 
	void Awake()
	{
		saveSystem = FindObjectOfType<SaveSystem>();
		settingSystem = FindObjectOfType<SettingSystem>();
	}

	void Start()
	{
		EnableContinueIfNeeded();
		SetGarlicModeToggle();
	}

	private void EnableContinueIfNeeded()
	{
		continueButton.SetActive(saveSystem.HasScene());
	}

	private void SetGarlicModeToggle()
	{
		bool isGarlicLoverMode = saveSystem.GetGarlicLoverMode();
		settingSystem.SetGarlicLoverMode(isGarlicLoverMode);
		garlicLoverModeToggle.isOn = isGarlicLoverMode;
	}
}
