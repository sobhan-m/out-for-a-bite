using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButtonManager : ModalManager
{
    private SaveSystem saveSystem;
	void Awake()
	{
        saveSystem = FindObjectOfType<SaveSystem>();
	}

    public void TryStartNewGame()
    {
        if (saveSystem.HasScene())
        {
            OpenModal();
        } else
        {
            SceneChangeManager.LoadNewGameScene();
        }
    }
}
