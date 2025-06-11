using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    public float GetDamageAmount();

    public bool HasTakenDamage(IDamageable damageable);

    public void AddToObjectsTakenDamage(IDamageable damageable);

    public bool ShouldTargetPlayer();
    public bool ShouldTargetEnemies();
}
