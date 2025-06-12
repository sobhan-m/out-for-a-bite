using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementManager))]
[RequireComponent(typeof(Collider2D))]
public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject meleeAttack;
    [SerializeField] private float damageAmount;
    [SerializeField] private float attackDistance;
    [SerializeField] private float secondsToAttack;
    private bool isAttacking = false;
    private EnemyMovementManager movementManager;
    private Rigidbody2D rb;
    private Vector3 targetPosition;

    private void Awake()
    {
        this.movementManager = GetComponent<EnemyMovementManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryAttack(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryAttack(collision);
    }

    private void TryAttack(Collider2D collision)
    {
        if (isAttacking)
        {
            return;
        }
        if (!collision.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager player))
        {
            return;
        }
        StartAttack(player.transform.position);
        Invoke("ExecuteAttack", secondsToAttack);
    }

    private void StartAttack(Vector3 targetPosition)
    {
        isAttacking = true;
        movementManager.Halt();
        movementManager.enabled = false;
        this.targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void ExecuteAttack()
    {
        Vector3 attackCentre = (targetPosition - transform.position).normalized * attackDistance + transform.position;
        Instantiate(meleeAttack, attackCentre, Quaternion.identity);

        EndAttack();
    }

    private void EndAttack()
    {
        isAttacking = false;
        movementManager.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
