using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMovementManager))]
[RequireComponent(typeof(Collider2D))]
public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject meleeAttack;
    [SerializeField] private float damageAmount;
    [SerializeField] private float attackDistance;
    private bool isAttacking = false;
    private EnemyMovementManager movementManager;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private EnemyTarget player;

    public UnityEvent startMeleeAttack;
    public UnityEvent endMeleeAttack;

    private void Awake()
    {
        this.movementManager = GetComponent<EnemyMovementManager>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<EnemyTarget>();
    }

    void Update()
    {
        Vector2 distanceFromPlayer = player.transform.position - transform.position;
        if (player.enabled && distanceFromPlayer.magnitude <= attackDistance)
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
    }

    private void StartAttack(Vector3 targetPosition)
    {
        startMeleeAttack.Invoke();
        isAttacking = true;
        this.targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
    }

    private void EndAttack()
    {
        isAttacking = false;
        endMeleeAttack.Invoke();
    }

    // ====================================
    // PUBLIC METHODS
    // ====================================
    
    public void ExecuteAttack()
    {
        Vector3 attackCentre = (targetPosition - transform.position).normalized * attackDistance + transform.position;

        Vector3 vectorToAttack = attackCentre - transform.position;
        float angleDegrees = Mathf.Atan2(vectorToAttack.y, vectorToAttack.x) * Mathf.Rad2Deg;

        Instantiate(meleeAttack, attackCentre, Quaternion.Euler(0, 0, angleDegrees));

        EndAttack();
    }
}
