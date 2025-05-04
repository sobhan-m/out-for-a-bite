using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fang : MonoBehaviour, IPickupable
{
    public void PickUp()
    {
        FangCounter.IncrementCount();
        Destroy(gameObject);
    }
}
