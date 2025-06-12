using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageDealer
{
    [SerializeField] public float damageAmount;
    [SerializeField] public float speed;

    public float GetDamageAmount()
    {
        return damageAmount;
    }

    public void AddToObjectsTakenDamage(IDamageable damageable)
    {
        
    }

    public bool HasTakenDamage(IDamageable damageable)
    {
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerService.WALL_LAYER))
        {
            Destroy(this.gameObject);
        }
    }

    public bool ShouldDamagePlayer()
    {
        return false;
    }

    public bool ShouldDamageEnemies()
    {
        return true;
    }
}
