using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessModeToggleManager : MonoBehaviour
{
    [SerializeField]
    private bool shouldDisableOnEndlessMode;
    [SerializeField]
    private GameObject objectToLoad;
    private EndlessScoreTracker endlessScoreTracker;
    private void Awake() {
        if (objectToLoad == null)
        {
            objectToLoad = gameObject;
        }
        endlessScoreTracker = FindObjectOfType<EndlessScoreTracker>();
        bool isEndless = endlessScoreTracker != null;
        if (isEndless && shouldDisableOnEndlessMode)
        {
            objectToLoad.SetActive(false);   
        } else if (!isEndless && !shouldDisableOnEndlessMode)
        {
            objectToLoad.SetActive(false);
        } else
        {
            objectToLoad.SetActive(true);
        }
    }
}
