using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMeleeController : MonoBehaviour
{
    [SerializeField] GameObject meleeAttackPrefab;
    [SerializeField] private float secondsToAttack;
    [SerializeField] private float attackDistance;
    private bool isAttacking = false;
    private InputAction meleeAction;
    private PlayerShootingController shootingController;

    public UnityEvent meleeEvent;

    private void Awake()
    {
        meleeAction = FindObjectOfType<InputActionContainingSystem>().actions.FindActionMap("Player").FindAction("Melee");
        meleeAction.performed += OnAttack;

        shootingController = FindObjectOfType<PlayerShootingController>();
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
        if (isAttacking) {
            return;
        }

        meleeEvent.Invoke();

        isAttacking = true;
        shootingController.enabled = false;
        Invoke("Attack", secondsToAttack);
    }

    private void Attack()
    {
        Vector3 attackCentre = InputService.GetDifferenceFromMouse(transform.position).normalized * attackDistance + transform.position;
        Instantiate(meleeAttackPrefab, attackCentre, Quaternion.Euler(0, 0, InputService.FindDegreeFromMouse(transform.position)));
        isAttacking = false;
        shootingController.enabled = true;
    }
}
