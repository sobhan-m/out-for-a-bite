using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject continueButton;
    private SaveSystem saveSystem;
	void Awake()
	{
		saveSystem = FindObjectOfType<SaveSystem>();
	}

	void Start()
	{
        continueButton.SetActive(saveSystem.HasScene());
	}




}
