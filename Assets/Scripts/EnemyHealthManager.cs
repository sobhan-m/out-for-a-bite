using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class EnemyHealthManager : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField][Min(0)] public float maxHealth;
    private Meter health;

    // Start is called before the first frame update
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageDealer damageDealer = collision.GetComponent<IDamageDealer>();
        if (damageDealer == null)
        {
            return;
        }

        TakeDamage(damageDealer.GetDamageAmount());

        if (collision.GetComponent<Bullet>() != null)
        {
            GameObject.Destroy(collision.gameObject);
        }
    }


}
