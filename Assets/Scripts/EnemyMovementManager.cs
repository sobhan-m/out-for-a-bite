using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovementManager : MonoBehaviour
{
    [SerializeField] private float speed;
    private EnemyTarget enemyTarget;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        enemyTarget = FindClosestEnemyTarget();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (enemyTarget == null)
        {
            return;
        }


        Vector3 vectorToTarget = enemyTarget.transform.position - transform.position;
        AdjustSpriteToFollowEnemy(vectorToTarget.x <= 0);

        Vector2 velocity = vectorToTarget.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);
    }

    private EnemyTarget FindClosestEnemyTarget()
    {
        EnemyTarget[] targets = FindObjectsOfType<EnemyTarget>();
        EnemyTarget closestEnemyTarget = null;
        float closestDistance = float.MaxValue;
        foreach (EnemyTarget target in targets)
        {
            float distance = (target.gameObject.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemyTarget = target;
            }
        }
        return closestEnemyTarget;
    }

    private void AdjustSpriteToFollowEnemy(bool isLeft)
    {
        spriteRenderer.flipX = isLeft;
    }

    // ====================================
    // PUBLIC METHODS
    // ====================================

    public void Halt()
    {
        rb.velocity = Vector3.zero;
    }
}
