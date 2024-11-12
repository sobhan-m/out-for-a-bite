using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FangCounter : MonoBehaviour
{
    public static int count { get; private set; } = 0;

    public static void IncrementCount()
    {
        IncrementCount(1);
    }

    public static void IncrementCount(int count)
    {
        FangCounter.count += count;
    }

    public static void DecrementCount()
    {
        DecrementCount(1);
    }

    public static void DecrementCount(int count) 
    { 
        FangCounter.count -= count; 
    }
}
