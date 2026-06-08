using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickupable
{
    [SerializeField] private int ammoCount = 1;
    public void PickUp()
    {
        FindObjectOfType<BulletReserve>().garlicCount += ammoCount;
        Destroy(gameObject);
    }
}
