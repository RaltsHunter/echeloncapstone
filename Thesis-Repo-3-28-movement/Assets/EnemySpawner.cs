using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your Enemy prefab
    public GridManager gridManager;

    void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        List<Vector2Int> walkablePositions = new List<Vector2Int>();

        // Gather all walkable tile coordinates
        foreach (Transform tile in gridManager.transform)
        {
            Labeller labeller = tile.GetComponent<Labeller>();
            if (labeller != null && tile.CompareTag("Tile"))
            {
                walkablePositions.Add(labeller.cords);
            }
        }

        int enemyCount = Random.Range(1, 6); // Spawn between 1 and 5 enemies

        for (int i = 0; i < enemyCount && walkablePositions.Count > 0; i++)
        {
            int index = Random.Range(0, walkablePositions.Count);
            Vector2Int spawnPosition = walkablePositions[index];
            walkablePositions.RemoveAt(index); // Avoid duplicate positions

            Vector3 worldPos = new Vector3(
                spawnPosition.x * gridManager.UnityGridSize,
                0.5f,
                spawnPosition.y * gridManager.UnityGridSize
            );

            GameObject enemy = Instantiate(enemyPrefab, worldPos, Quaternion.identity);

            // Name the enemy
            Unit unit = enemy.GetComponent<Unit>();
            if (unit != null)
            {
                unit.unitName = $"Enemy {i + 1}";
                enemy.name = unit.unitName;
            }

            // Manually initialize AI
            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.ManualInitialize();
            }
        }
    }
}

