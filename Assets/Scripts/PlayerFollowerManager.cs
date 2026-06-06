using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerManager : MonoBehaviour
{

    private PlayerMovementController player;
	void Awake()
	{
		player = FindObjectOfType<PlayerMovementController>();
	}

	private void LateUpdate()
    {
        this.transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}
