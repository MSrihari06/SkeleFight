using UnityEngine;

public class Enemy_Respawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown = 2f;
    private float timer;
    [SerializeField] private float cooldownDecreaseRate = .5f;
    [SerializeField] private float cooldownCap = .7f;
    private Transform player;

    private void Awake()
    {
        // Optimized: Find the player object once and then get the transform
        Player playerComponent = FindFirstObjectByType<Player>();

        if (playerComponent != null)
        {
            player = playerComponent.transform;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = cooldown;
            CreateNewEnemy();

            // Gradually decrease cooldown until it hits the cap
            cooldown = Mathf.Max(cooldownCap, cooldown - cooldownDecreaseRate);
        }
    }

    private void CreateNewEnemy()
    {
        // 1. Safety check for the array
        if (respawnPoints == null || respawnPoints.Length == 0)
        {
            Debug.LogWarning("No respawn points assigned to the Enemy_Respawner!");
            return;
        }

        // 2. Select a random spawn point
        int randomIndex = Random.Range(0, respawnPoints.Length);
        Vector3 spawnPosition = respawnPoints[randomIndex].position;

        // 3. Spawn the enemy prefab
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 4. Handle direction logic
        if (player != null)
        {
            // Check if spawned to the right of the player
            bool createdOnTheRight = newEnemy.transform.position.x > player.position.x;

            if (createdOnTheRight)
            {
                // Must match 'enemy' (lowercase) from your enemy.cs file
                enemy enemyScript = newEnemy.GetComponent<enemy>();

                if (enemyScript != null)
                {
                    enemyScript.flip();
                }
                else
                {
                    Debug.LogError("The spawned prefab is missing the 'enemy' script!");
                }
            }
        }
    }
} // <--- This was the missing bracket causing your error!