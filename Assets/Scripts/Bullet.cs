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
}
