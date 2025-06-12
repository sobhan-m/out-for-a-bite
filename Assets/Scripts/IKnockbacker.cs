using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbacker
{
    public void PushAwayFromSource(Rigidbody2D rb);

    public bool ShouldKnockbackPlayer();
    public bool ShouldKnockbackEnemies();
}
