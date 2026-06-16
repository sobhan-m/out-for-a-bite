
using Unity.VisualScripting;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    [Header("General")]

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform spawnLocation;
    [SerializeField] [Min(0)]
    private float minimumDistanceFromPlayer;

    [Header("Before Checklist Complete")]

    [SerializeField] [Min(0)]
    private float minimumSecondsBetweenSpawnBeforeAllIngredients;
    [SerializeField] [Min(0)]
    private float maximumSecondsBetweenSpawnBeforeAllIngredients;

    [Header("After Checklist Complete")]

    [SerializeField] [Min(0)]
    private float minimumSecondsBetweenSpawnAfterAllIngredients;
    [SerializeField] [Min(0)]
    private float maximumSecondsBetweenSpawnAfterAllIngredients;

    private PlayerMovementController player;
    private ChecklistSystem checklistSystem;
    private Meter spawnCooldown;

	void Awake()
	{
		player = FindObjectOfType<PlayerMovementController>();
		checklistSystem = FindObjectOfType<ChecklistSystem>();
	}

	void Start()
    {
        if (spawnLocation == null)
        {
            spawnLocation = this.transform;
        }
        RefillCooldown();
    }

    void Update()
    {
        Vector2 vectorToPlayer = player.transform.position - spawnLocation.position;
        float distanceFromPlayer = Mathf.Abs(vectorToPlayer.magnitude);
        if (spawnCooldown.IsEmpty() && distanceFromPlayer > minimumDistanceFromPlayer)
        {
            GameObject.Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
            RefillCooldown();
        }
        spawnCooldown.EmptyMeter(Time.deltaTime);
    }

    private void RefillCooldown()
    {
        float minToUse;
        float maxToUse;
        if (checklistSystem.IsChecklistComplete())
        {
            minToUse = minimumSecondsBetweenSpawnAfterAllIngredients;
            maxToUse = maximumSecondsBetweenSpawnAfterAllIngredients;
        } else
        {
            minToUse = minimumSecondsBetweenSpawnBeforeAllIngredients;
            maxToUse = maximumSecondsBetweenSpawnBeforeAllIngredients;
        }

        float cooldown = Random.Range(minToUse, maxToUse);
        spawnCooldown = new Meter(0, cooldown, cooldown);
    }
}
