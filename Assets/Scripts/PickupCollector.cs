using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickupable pickup = collision.GetComponent<IPickupable>();

        if (pickup == null)
        {
            return;
        }

        pickup.PickUp();
    }
}
