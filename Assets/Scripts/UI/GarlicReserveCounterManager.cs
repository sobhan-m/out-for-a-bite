using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletReserveCounterManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int cachedCount;

    private void Start()
    {
        cachedCount = GarlicReserve.instance.garlicCount;
        text.text = cachedCount.ToString();
    }

    void Update()
    {
        if (cachedCount != GarlicReserve.instance.garlicCount)
        {
            cachedCount = GarlicReserve.instance.garlicCount;
            text.text = cachedCount.ToString();
        }
    }
}
