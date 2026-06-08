using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicReserve : MonoBehaviour
{
    [SerializeField] public int garlicCount = 12;

    public bool HasGarlic()
    {
        return garlicCount > 0;
    }

    public void UseGarlic()
    {
        --garlicCount;
    }
}
