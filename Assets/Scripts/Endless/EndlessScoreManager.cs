using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndlessScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int cachedCount;
    
    void Start()
    {
        cachedCount = EndlessScoreTracker.instance.GetLevelsCompleted();
    }

    void Update()
    {
        text.text = EndlessScoreTracker.instance.GetLevelsCompleted().ToString();
    }
}
