using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletReserveCounterManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int cachedCount;
    private BulletReserve bulletReserve;

    private void Awake()
    {
        bulletReserve = FindObjectOfType<BulletReserve>();
        cachedCount = bulletReserve.bulletCount;
        text.text = cachedCount.ToString();
    }

    void Update()
    {
        if (cachedCount != bulletReserve.bulletCount)
        {
            cachedCount = bulletReserve.bulletCount;
            text.text = cachedCount.ToString();
        }
    }
}
