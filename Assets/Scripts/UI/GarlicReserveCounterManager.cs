using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletReserveCounterManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int cachedCount;
    private GarlicReserve garlicReserve;

    private void Awake()
    {
        garlicReserve = FindObjectOfType<GarlicReserve>();
        cachedCount = garlicReserve.garlicCount;
        text.text = cachedCount.ToString();
    }

    void Update()
    {
        if (cachedCount != garlicReserve.garlicCount)
        {
            cachedCount = garlicReserve.garlicCount;
            text.text = cachedCount.ToString();
        }
    }
}
