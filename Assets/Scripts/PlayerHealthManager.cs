using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField][Min(0)] private float maxHealth;
    private Meter health;

    void Start()
    {
        health = new Meter(0, maxHealth, maxHealth);
    }

    public void Die()
    {
        // TODO: Go to game over screen.
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
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageDealer damageDealer = collision.GetComponent<IDamageDealer>();
        if (damageDealer == null)
        {
            return;
        }

        if (!damageDealer.HasTakenDamage(this) && damageDealer.ShouldDamagePlayer())
        {
            TakeDamage(damageDealer.GetDamageAmount());
            damageDealer.AddToObjectsTakenDamage(this);
        }
    }
}
