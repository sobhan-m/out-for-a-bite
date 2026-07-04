using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicPickup : MonoBehaviour, IPickupable
{
    [SerializeField] private int ammoCount = 1;
    public void PickUp()
    {
        GarlicReserve.instance.garlicCount += ammoCount;
        Destroy(gameObject);
    }
}
