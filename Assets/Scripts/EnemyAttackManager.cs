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
    private PlayerHealthManager player;
    private Animator animator;

    private void Awake()
    {
        this.movementManager = GetComponent<EnemyMovementManager>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerHealthManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 distanceFromPlayer = player.transform.position - transform.position;
        if (distanceFromPlayer.magnitude <= attackDistance)
        {
            TryAttack();
        }
	}

	private void TryAttack()
    {
        if (isAttacking)
        {
            return;
        }
        StartAttack(player.transform.position);
        Invoke("ExecuteAttack", secondsToAttack);
    }

    private void StartAttack(Vector3 targetPosition)
    {
        animator.SetTrigger(AnimationService.MELEE_ATTACK);
        isAttacking = true;
        movementManager.Halt();
        movementManager.enabled = false;
        this.targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
