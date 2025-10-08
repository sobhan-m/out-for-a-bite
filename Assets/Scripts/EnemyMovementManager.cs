using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovementManager : MonoBehaviour
{
    [SerializeField] private float targetRange;
    [SerializeField] private float speed;
    private EnemyTarget enemyTarget;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isHalted = false;
    private bool isWalking = false;

    public UnityEvent<bool> isWalkingEvent;

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
        if (FindDistanceToTarget() <= targetRange)
        {
            Move();
        }
    }

    private void Move()
    {
        if (enemyTarget == null || !enemyTarget.enabled || isHalted)
        {
            TrySetIsWalking(false);
            return;
        }
        TrySetIsWalking(true);

        Vector3 vectorToTarget = FindVectorToTarget();
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

    private Vector3 FindVectorToTarget()
    {
        return enemyTarget.transform.position - transform.position;
    }

    private float FindDistanceToTarget()
    {
        return FindVectorToTarget().magnitude;
    }

    private void TrySetIsWalking(bool isWalking) {
        if (this.isWalking != isWalking)
        {
            this.isWalking = isWalking;
            isWalkingEvent.Invoke(this.isWalking);
        }
    }

    // ====================================
    // PUBLIC METHODS
    // ====================================

    public void Halt()
    {
        isHalted = true;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    public void Proceed()
    {
        isHalted = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
