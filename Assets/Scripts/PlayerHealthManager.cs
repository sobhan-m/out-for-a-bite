using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] private float damageTakenIndicationDuration;
    [SerializeField][Min(0)] private float maxHealth;
    private Meter health;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        health = new Meter(0, maxHealth, maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        AddDamageTakenIndication();
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

    private void AddDamageTakenIndication()
    {
        spriteRenderer.color = Color.black;
        Invoke("RemoveDamageTakenIndication", damageTakenIndicationDuration);
    }

    private void RemoveDamageTakenIndication()
    {
        spriteRenderer.color = Color.white;
    }
}
