using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsManager : MonoBehaviour
{
    private InputAction nextAction;
    [SerializeField]
    private float secondsToWait = 2f;
    private float secondsPassed = 0f;

	void Awake()
	{
		nextAction = FindObjectOfType<InputActionContainingSystem>().actions.FindActionMap("Credits").FindAction("Next");
	}

	void Update()
	{
		secondsPassed = Mathf.Clamp(secondsPassed + Time.deltaTime, 0f, secondsToWait);
	}

	private void ReturnToMainMenu(InputAction.CallbackContext context)
    {
        if (secondsPassed >= secondsToWait)
        {
            SceneChangeManager.LoadScene(SceneChangeManager.MAIN_MENU);
        }
    }

    private void OnEnable()
    {
        nextAction.performed += ReturnToMainMenu;
        nextAction.Enable();
    }

    private void OnDisable()
    {
        nextAction.performed -= ReturnToMainMenu;
        nextAction.Disable();
    }
}
