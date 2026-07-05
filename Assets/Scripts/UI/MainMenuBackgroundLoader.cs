using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MainMenuBackgroundLoader : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> backgrounds;

	void Awake()
	{
        if (backgrounds.Count > 1)
        {
            backgrounds.Sort(new RandomSorter());
        }
        GetComponent<Image>().sprite = backgrounds[0];
	}
}
