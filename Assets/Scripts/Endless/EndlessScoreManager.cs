using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndlessScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int cachedCount;
    private EndlessScoreTracker scoreTracker;
    
    void Start()
    {
        scoreTracker = FindObjectOfType<EndlessScoreTracker>();
        cachedCount = scoreTracker.GetLevelsCompleted();
    }

    void Update()
    {
        if (cachedCount != scoreTracker.GetLevelsCompleted())
        {
            cachedCount = scoreTracker.GetLevelsCompleted();
            text.text = cachedCount.ToString();
        }
    }
}
