using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FangCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fang fang = collision.GetComponent<Fang>();


        if (fang == null )
        {
            return;
        }

        fang.PickUp();
    }
}
