using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField]
    private float secondsForFade = 0.1f;
    private CanvasGroup loadingScreen;
	void Awake()
	{
		loadingScreen = GetComponent<CanvasGroup>();
	}

	void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        loadingScreen.alpha = 1;
        float secondsElapsed = 0f;
        while (secondsElapsed < secondsForFade)
        {
            secondsElapsed += Time.deltaTime;
            loadingScreen.alpha = Mathf.Clamp((secondsForFade - secondsElapsed)/secondsForFade, 0f, 1f);
            yield return null;
        }

        loadingScreen.alpha = 0;
    }

    private IEnumerator FadeIn()
    {
        loadingScreen.alpha = 0;
        float secondsElapsed = 0f;

        while (secondsElapsed < secondsForFade)
        {
            secondsElapsed += Time.deltaTime;
            loadingScreen.alpha = Mathf.Clamp(secondsElapsed/secondsForFade, 0f, 1f);
            yield return null;
        }

        loadingScreen.alpha = 1;
    }

    public void FadeInLoadingScreen()
    {
        StartCoroutine(FadeIn());
    }
}
