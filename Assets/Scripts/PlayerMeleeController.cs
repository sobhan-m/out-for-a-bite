using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeController : MonoBehaviour
{
    [SerializeField] GameObject meleeAttackPrefab;
    [SerializeField] private float secondsToAttack;
    [SerializeField] private float attackDistance;
    private bool isAttacking = false;
    private InputAction meleeAction;
    private PlayerShootingController shootingController;
    private Animator animator;

    private void Awake()
    {
        meleeAction = FindObjectOfType<InputActionContainingSystem>().actions.FindActionMap("Player").FindAction("Melee");
        meleeAction.performed += OnAttack;

        shootingController = GetComponent<PlayerShootingController>();
        animator = GetComponent<Animator>();
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

        animator.SetTrigger(AnimationService.MELEE_ATTACK);

        isAttacking = true;
        shootingController.enabled = false;
        Invoke("Attack", secondsToAttack);
    }

    private void Attack()
    {
        Vector3 attackCentre = InputService.GetDifferenceFromMouse(transform.position).normalized * attackDistance + transform.position;
        Instantiate(meleeAttackPrefab, attackCentre, Quaternion.identity);
        isAttacking = false;
        shootingController.enabled = true;
    }
}
