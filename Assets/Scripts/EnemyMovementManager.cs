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

    // Start is called before the first frame update
    void Start()
    {
        enemyTarget = FindClosestEnemyTarget();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (enemyTarget == null)
        {
            throw new MissingReferenceException("Unable to find an enemy in the scene.");
        }


        Vector3 vectorToTarget = enemyTarget.transform.position - transform.position;
        AdjustSpriteToFollowEnemy(vectorToTarget.x <= 0);

        Vector3 velocity = vectorToTarget.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + velocity);
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
}
