using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerManager : MonoBehaviour
{
    public GameObject player;

    private void LateUpdate()
    {
        this.transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}
