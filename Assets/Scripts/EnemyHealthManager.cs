using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class EnemyHealthManager : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField][Min(0)] public float maxHealth;
    private Meter health;

    void Start()
    {
        health = new Meter(0, maxHealth, maxHealth);
    }

    public void Die()
    {
        FangDropper fangDropper = GetComponent<FangDropper>();
        fangDropper.DropFangs();

        Destroy(gameObject);
    }

    public void ReceiveHeal(float heal)
    {
        health.FillMeter(heal);
    }

    public void TakeDamage(float damage)
    {
        health.EmptyMeter(damage);
        if (health.IsEmpty())
        {
            Die();
        }
    }

    public bool IsPlayer()
    {
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleDamageDealer(collision);
        HandleBullet(collision);
    }

    private void HandleDamageDealer(Collider2D collision)
    {
        IDamageDealer damageDealer = collision.GetComponent<IDamageDealer>();
        if (damageDealer == null)
        {
            return;
        }

        if (!damageDealer.HasTakenDamage(this) && damageDealer.ShouldDamageEnemies())
        {
            TakeDamage(damageDealer.GetDamageAmount());
            damageDealer.AddToObjectsTakenDamage(this);
        }
    }

    private void HandleBullet(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            GameObject.Destroy(collision.gameObject);
        }
    }
}
