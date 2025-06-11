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

    private void Awake()
    {
        meleeAction = FindObjectOfType<InputActionContainingSystem>().actions.FindActionMap("Player").FindAction("Melee");
        meleeAction.performed += OnAttack;
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

        isAttacking = true;
        Invoke("Attack", secondsToAttack);
    }

    private void Attack()
    {
        Vector3 attackCentre = InputService.GetDifferenceFromMouse(transform.position).normalized * attackDistance + transform.position;
        Instantiate(meleeAttackPrefab, attackCentre, Quaternion.identity);
        isAttacking = false;
    }
}
