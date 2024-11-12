using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class FangCollector : MonoBehaviour
{
    private Collision2D collision2D;
    void Start()
    {
        collision2D = GetComponent<Collision2D>();
    }

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
