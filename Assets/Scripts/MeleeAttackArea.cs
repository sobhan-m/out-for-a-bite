using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MeleeAttackArea : MonoBehaviour, IDamageDealer
{
    [SerializeField] private float damageAmount;
    [SerializeField] private float secondsBeforeDespawning;
    [SerializeField] private bool shouldTargetPlayer;
    [SerializeField] private bool shouldTargetEnemies;
    private List<IDamageable> targets = new List<IDamageable>();

    private void Start()
    {
        Destroy(gameObject, secondsBeforeDespawning);
    }

    public void AddToObjectsTakenDamage(IDamageable damageable)
    {
        targets.Add(damageable);
    }

    public float GetDamageAmount()
    {
        return damageAmount;
    }

    public bool HasTakenDamage(IDamageable damageable)
    {
        return targets.Contains(damageable);
    }

    public bool ShouldTargetPlayer()
    {
        return shouldTargetPlayer;
    }

    public bool ShouldTargetEnemies()
    {
        return shouldTargetEnemies;
    }
}
