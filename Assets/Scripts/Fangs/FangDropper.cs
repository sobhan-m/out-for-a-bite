using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FangDropper : MonoBehaviour
{
    [SerializeField] private GameObject fangPrefab;
    [SerializeField] private int fangsToTryToDrop;
    [SerializeField] [Range(0f, 1f)] private float percentageOfFangDrop;

    public void DropFangs()
    {
        for (int i = 0; i < fangsToTryToDrop; i++)
        {
            if (Random.Range(0f, 1f) > percentageOfFangDrop)
            {
                Instantiate(fangPrefab, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
