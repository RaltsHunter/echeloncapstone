using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GridManager gridManager;
    private Unit unit;
    private CombatManager combatManager;
    private ExplorationManager explorationManager;
    private bool snappedToTile = false;

    private List<Vector2Int> patrolPoints = new List<Vector2Int>();
    private int patrolIndex = 0;
    private float patrolSpeed = 2f;
    private float reachThreshold = 0.1f;
    private float patrolStepDelay = 0.5f;
    private Coroutine patrolRoutine;

    private bool patrolForward = true;



    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        unit = GetComponent<Unit>();
        combatManager = FindObjectOfType<CombatManager>();
        explorationManager = FindObjectOfType<ExplorationManager>();

        SnapToNearestWalkableTile();
        GeneratePatrolPoints();
        ManualInitialize();
    }

    private void Update()
    {
        if (!snappedToTile || combatManager == null) return;

        if (!combatManager.combatActive && patrolPoints.Count > 0)
        {
            if (patrolRoutine == null)
            {
                patrolRoutine = StartCoroutine(PatrolRoutine());
            }

            if (DetectNearbyPlayer())
            {
                combatManager.TriggerCombat();

                // Stop patrol immediately on combat
                if (patrolRoutine != null)
                {
                    StopCoroutine(patrolRoutine);
                    patrolRoutine = null;
                }
            }
        }
        else
        {
            // Stop patrol if combat starts
            if (patrolRoutine != null)
            {
                StopCoroutine(patrolRoutine);
                patrolRoutine = null;
            }
        }
    }
    public bool IsNearPlayerForCombat()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 3.0f) // Adjust this threshold as needed
            {
                return true;
            }
        }
        return false;
    }


    private void SnapToNearestWalkableTile()
    {
        Vector2Int approxGridPos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize),
            Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize)
        );

        Vector2Int bestSpot = FindNearestWalkableTile(approxGridPos);
        transform.position = new Vector3(
            bestSpot.x * gridManager.UnityGridSize,
            transform.position.y,
            bestSpot.y * gridManager.UnityGridSize
        );

        snappedToTile = true;
    }

    private Vector2Int FindNearestWalkableTile(Vector2Int center)
    {
        int maxRadius = 2;
        for (int radius = 0; radius <= maxRadius; radius++)
        {
            for (int dx = -radius; dx <= radius; dx++)
            {
                for (int dy = -radius; dy <= radius; dy++)
                {
                    Vector2Int checkPos = center + new Vector2Int(dx, dy);
                    GameObject tile = gridManager.GetTileGameObjectAtPosition(checkPos);

                    if (tile != null && !tile.CompareTag("Unwalkable"))
                    {
                        return checkPos;
                    }
                }
            }
        }

        Debug.LogWarning("No walkable tile found nearby. Using current position.");
        return center;
    }

    private void GeneratePatrolPoints()
    {
        Vector2Int origin = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize),
            Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize)
        );

        patrolPoints.Clear();
        int radius = 3;

        List<Vector2Int> horizontal = new List<Vector2Int>();
        List<Vector2Int> vertical = new List<Vector2Int>();

        // Scan horizontal
        for (int dx = -radius; dx <= radius; dx++)
        {
            Vector2Int checkPos = new Vector2Int(origin.x + dx, origin.y);
            if (dx == 0) continue; // skip center, add later
            GameObject tile = gridManager.GetTileGameObjectAtPosition(checkPos);
            if (tile != null && !tile.CompareTag("Unwalkable"))
            {
                horizontal.Add(checkPos);
            }
        }

        // Scan vertical
        for (int dy = -radius; dy <= radius; dy++)
        {
            Vector2Int checkPos = new Vector2Int(origin.x, origin.y + dy);
            if (dy == 0) continue; // skip center, add later
            GameObject tile = gridManager.GetTileGameObjectAtPosition(checkPos);
            if (tile != null && !tile.CompareTag("Unwalkable"))
            {
                vertical.Add(checkPos);
            }
        }

        // Choose the longer path
        List<Vector2Int> selectedPath = (horizontal.Count >= vertical.Count) ? horizontal : vertical;

        // Add origin back into patrol loop if it's walkable
        GameObject originTile = gridManager.GetTileGameObjectAtPosition(origin);
        if (originTile != null && !originTile.CompareTag("Unwalkable"))
        {
            selectedPath.Add(origin);
        }

        // Sort path for consistent movement order
        selectedPath.Sort((a, b) =>
        {
            // Horizontal: sort by x, vertical: sort by y
            return (horizontal.Count >= vertical.Count)
                ? a.x.CompareTo(b.x)
                : a.y.CompareTo(b.y);
        });

        patrolPoints = selectedPath;

        if (patrolPoints.Count == 0)
        {
            Debug.LogError("No walkable patrol points found. Enemy will stand still.");
        }
        else
        {
            Debug.Log($"Generated {patrolPoints.Count} patrol points ({(horizontal.Count >= vertical.Count ? "horizontal" : "vertical")}).");
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Count == 0) return;

        Vector2Int targetGridPos = patrolPoints[patrolIndex];

        // Instantly snap to the target tile
        transform.position = new Vector3(
            targetGridPos.x * gridManager.UnityGridSize,
            transform.position.y,
            targetGridPos.y * gridManager.UnityGridSize
        );

        // Determine next index based on direction
        if (patrolForward)
        {
            patrolIndex++;
            if (patrolIndex >= patrolPoints.Count)
            {
                patrolIndex = patrolPoints.Count - 2;
                patrolForward = false;
            }
        }
        else
        {
            patrolIndex--;
            if (patrolIndex < 0)
            {
                patrolIndex = 1;
                patrolForward = true;
            }
        }
    }


    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            Patrol();
            yield return new WaitForSeconds(patrolStepDelay);
        }
    }




    private bool DetectNearbyPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 2.0f)
            {
                if (TriggerCombat.Instance != null)
                {
                    TriggerCombat.Instance.TriggerCombatStart(); // ✅ Activates CombatManagerParent and disables Exploration
                }
                else
                {
                    Debug.LogWarning("TriggerCombat.Instance is null — cannot activate combat UI.");
                }

                if (combatManager != null)
                {
                    combatManager.TriggerCombat(); // ✅ Starts logic flow of combat
                }
                else
                {
                    Debug.LogWarning("CombatManager is null — cannot trigger combat logic.");
                }

                return true;
            }
        }
        return false;
    }



    public IEnumerator EnemyTurn()
    {
        while (unit.HasMovementPoints())
        {
            MoveTowardsNearestPlayer();

            if (AttackNearestPlayerUnit())
            {
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void MoveTowardsNearestPlayer()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        GameObject nearestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject player in playerUnits)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null)
        {
            Vector2Int currentGridPosition = new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize)
            );

            Vector2Int targetGridPosition = new Vector2Int(
                Mathf.RoundToInt(nearestPlayer.transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(nearestPlayer.transform.position.z / gridManager.UnityGridSize)
            );

            int xDiff = Mathf.Abs(targetGridPosition.x - currentGridPosition.x);
            int yDiff = Mathf.Abs(targetGridPosition.y - currentGridPosition.y);

            if ((xDiff == 1 && yDiff == 0) || (xDiff == 0 && yDiff == 1))
            {
                unit.currentMovementPoints = 0;
                return;
            }

            Vector2Int direction = new Vector2Int(
                (targetGridPosition.x > currentGridPosition.x) ? 1 : (targetGridPosition.x < currentGridPosition.x) ? -1 : 0,
                (targetGridPosition.y > currentGridPosition.y) ? 1 : (targetGridPosition.y < currentGridPosition.y) ? -1 : 0
            );

            if (direction.x != 0 && direction.y != 0)
            {
                direction.y = 0;
            }

            Vector2Int nextPos = currentGridPosition + direction;
            GameObject tile = gridManager.GetTileGameObjectAtPosition(nextPos);

            if (tile != null && tile.CompareTag("Tile") && unit.HasMovementPoints() && !IsPlayerOccupied(nextPos))
            {
                unit.MoveUnit(nextPos);
                transform.position = new Vector3(
                    nextPos.x * gridManager.UnityGridSize,
                    transform.position.y,
                    nextPos.y * gridManager.UnityGridSize
                );
            }
            else
            {
                unit.currentMovementPoints = 0;
            }
        }
    }

    private bool IsPlayerOccupied(Vector2Int position)
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject player in playerUnits)
        {
            Vector2Int playerGridPos = new Vector2Int(
                Mathf.RoundToInt(player.transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(player.transform.position.z / gridManager.UnityGridSize)
            );

            if (playerGridPos == position)
            {
                return true;
            }
        }
        return false;
    }

    private bool AttackNearestPlayerUnit()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        GameObject nearestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject player in playerUnits)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null)
        {
            Vector2Int currentGridPosition = new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize)
            );

            Vector2Int targetGridPosition = new Vector2Int(
                Mathf.RoundToInt(nearestPlayer.transform.position.x / gridManager.UnityGridSize),
                Mathf.RoundToInt(nearestPlayer.transform.position.z / gridManager.UnityGridSize)
            );

            int xDiff = Mathf.Abs(targetGridPosition.x - currentGridPosition.x);
            int yDiff = Mathf.Abs(targetGridPosition.y - currentGridPosition.y);

            if ((xDiff == 1 && yDiff == 0) || (xDiff == 0 && yDiff == 1))
            {
                Unit playerUnit = nearestPlayer.GetComponent<Unit>();
                if (playerUnit != null)
                {
                    unit.Attack(playerUnit);
                    Debug.Log($"Enemy attacked {playerUnit.gameObject.name} for {unit.attackDamage} damage.");
                    return true;
                }
            }
        }

        return false;
    }

    public void ManualInitialize()
    {
        gridManager = FindObjectOfType<GridManager>();
        unit = GetComponent<Unit>();
        combatManager = FindObjectOfType<CombatManager>();
        explorationManager = FindObjectOfType<ExplorationManager>();

        SnapToNearestWalkableTile();
        GeneratePatrolPoints();
        snappedToTile = true;
    }

}
