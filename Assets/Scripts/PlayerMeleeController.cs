using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] private float damageAmount;
    [SerializeField] private float secondsToAttack;
    [SerializeField] private float attackRadius;
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
        meleeAction.Disable();
    }

    private void OnDisable()
    {
        meleeAction.Enable();
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCentre, attackRadius);

        foreach (Collider2D collider in colliders)
        {
            if (!collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                continue;
            }
            // TODO: Add player health here.
            damageable.TakeDamage(damageAmount);
        }

        isAttacking = false;
    }
}
