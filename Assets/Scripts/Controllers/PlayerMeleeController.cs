using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMeleeController : MonoBehaviour
{
    [SerializeField] GameObject meleeAttackPrefab;
    [SerializeField] private float attackDistance;
    [SerializeField] private float secondsBetweenMelee;
    [SerializeField] private AudioClip meleeAttackSoundClip;
    private bool isAttacking = false;
    private InputAction meleeAction;
    private PlayerShootingController shootingController;
    private Camera cam;
    private Meter meleeCooldown;

    public UnityEvent meleeEvent;

    private void Awake()
    {
        cam = Camera.main;
        
        meleeAction = FindObjectOfType<InputActionContainingSystem>().actions.FindActionMap("Player").FindAction("Melee");
        meleeAction.performed += OnAttack;

        shootingController = FindObjectOfType<PlayerShootingController>();

        meleeCooldown = new Meter(0, secondsBetweenMelee);
    }

	void Update()
	{
		meleeCooldown.EmptyMeter(Time.deltaTime);
	}


	private void OnEnable()
    {
        meleeAction.Enable();
    }

    private void OnDisable()
    {
        meleeAction.Disable();
    }

    // ====================================
    // HELPERS
    // ====================================

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (isAttacking || PauseController.isPaused || !meleeCooldown.IsEmpty()) {
            return;
        }

        meleeEvent.Invoke();
        AudioSource.PlayClipAtPoint(meleeAttackSoundClip, cam.transform.position);

        meleeCooldown.FillMeter();
        isAttacking = true;
        shootingController.enabled = false; // TODO: control this via events. This class should be unaware of shooting.
    }

    // ====================================
    // PUBLIC METHODS
    // ====================================

    public void Attack()
    {
        Vector3 attackCentre = InputService.GetDifferenceFromMouse(transform.position, cam).normalized * attackDistance + transform.position;
        Instantiate(meleeAttackPrefab, attackCentre, Quaternion.Euler(0, 0, InputService.FindDegreeFromMouse(transform.position, cam)));
        isAttacking = false;
        shootingController.enabled = true;
    }
}
