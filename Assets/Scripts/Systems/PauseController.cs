using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private UnityEvent pauseEvent;
    [SerializeField] private UnityEvent resumeEvent;

    public static bool isPaused { private set; get; }
    private InputAction pause;
    private InputActionAsset actions;

    void Awake()
    {
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        pause = actions.FindActionMap("Player").FindAction("Pause");
    }

    void Update()
    {
        if (pause.triggered && isActive)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    private void Start()
	{
		Resume();
	}

	private void OnEnable()
	{
		pause.Enable();
		Resume();
	}

	private void OnDisable()
	{
		pause.Disable();
		Resume();
	}

	public void Pause()
	{
		isPaused = true;
        Time.timeScale = 0;
        pauseEvent.Invoke();
	}

	public void Resume()
	{
		isPaused = false;
        Time.timeScale = 1;
        resumeEvent.Invoke();
	}
}
