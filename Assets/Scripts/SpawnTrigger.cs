using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private bool canRetrigger;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered && !canRetrigger)
        {
            return;
        }
        if (!collision.gameObject.TryGetComponent<PlayerMovementController>(out PlayerMovementController player))
        {
            return;
        }

        hasTriggered = true;
        foreach (Transform spawnLocation in spawnLocations)
        {
            Instantiate(spawnObjectPrefab, spawnLocation.position, Quaternion.identity);
        }
    }
}
