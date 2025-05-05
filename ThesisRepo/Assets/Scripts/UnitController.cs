using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    GridManager gridManager;

    public GameObject selectedUnit;
    bool unitSelected = false;
    private int selectedUnitCurrentMovementPoints;
    [SerializeField] public bool isAttacking = false;
    public GameObject attackTarget;
    [SerializeField] LayerMask tileLayerMask;
    public GameObject cameraController;
    public InventoryManager inventoryManager;

    public Material highlightMaterial;
    public Material defaultMaterial;

    private List<GameObject> highlightedTiles = new List<GameObject>();

    public Material attackHighlightMaterial;
    private List<GameObject> attackHighlightedTiles = new List<GameObject>();


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        cameraController = GameObject.Find("Main Camera");
        inventoryManager = GameObject.Find("InventoryManager")?.GetComponent<InventoryManager>();

        if (gridManager == null) Debug.LogError("GridManager not found!");
        if (cameraController == null) Debug.LogError("Main Camera not found!");
        if (inventoryManager == null) Debug.LogError("InventoryManager not found!");
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on UI, ignoring.");
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity); // Detect all objects

            GameObject hitUnit = null;
            GameObject hitTile = null;

            foreach (RaycastHit hit in hits)
            {
                Debug.Log($"Hit: {hit.transform.name}, Tag: {hit.transform.tag}");

                if (hit.transform.CompareTag("PlayerUnit") && hitUnit == null)
                {
                    hitUnit = hit.transform.gameObject;
                }
                else if (hit.transform.CompareTag("Tile") && hitTile == null)
                {
                    hitTile = hit.transform.gameObject;
                }
            }

            // **Prioritize Unit Selection**
            if (hitUnit != null)
            {
                Debug.Log($"Selected Unit: {hitUnit.name}");
                ClearHighlightedTiles();
                ClearAttackHighlightedTiles();

                selectedUnit = hitUnit;
                unitSelected = true;
                selectedUnitCurrentMovementPoints = selectedUnit.GetComponent<Unit>().currentMovementPoints;

                if (selectedUnit.GetComponent<Unit>() == null)
                {
                    Debug.LogError("Selected unit has no Unit component!");
                }
                else
                {
                    Debug.Log($"Unit Selected: {selectedUnit.name}, Movement Points: {selectedUnitCurrentMovementPoints}");
                }

                HighlightReachableTiles();
                return;
            }

            // **Handle Attacking if in Attack Mode and clicked a red-highlighted tile**
            if (hitTile != null && isAttacking && attackHighlightedTiles.Contains(hitTile))
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.CompareTag("EnemyUnit"))
                    {
                        Unit enemyUnit = hit.transform.GetComponent<Unit>();
                        if (enemyUnit != null)
                        {
                            selectedUnit.GetComponent<Unit>().Attack(enemyUnit);
                            ClearAttackHighlightedTiles();
                            HighlightReachableTiles(); // Refresh movement tiles
                            return;
                        }
                    }
                }

                Debug.Log("No enemy unit found on attack tile.");
                return;
            }

            // **Handle Tile Movement**
            if (hitTile != null && unitSelected && selectedUnit != null && selectedUnitCurrentMovementPoints > 0)
            {
                Debug.Log($"Attempting to move {selectedUnit.name} to tile {hitTile.name}");

                Labeller tileLabeller = hitTile.GetComponent<Labeller>();
                if (tileLabeller == null)
                {
                    Debug.LogError("Tile has no Labeller component!");
                    return;
                }

                Vector2Int targetCords = tileLabeller.cords;
                Vector2Int startCords = new Vector2Int(
                    Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
                    Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
                );

                int manhattanDistance = Mathf.Abs(targetCords.x - startCords.x) + Mathf.Abs(targetCords.y - startCords.y);
                Debug.Log($"Manhattan Distance: {manhattanDistance}, Movement Points Available: {selectedUnitCurrentMovementPoints}");

                if (highlightedTiles.Contains(hitTile) && !attackHighlightedTiles.Contains(hitTile))
                {
                    ClearHighlightedTiles();
                    if (manhattanDistance <= selectedUnitCurrentMovementPoints)
                    {
                        selectedUnit.transform.position = new Vector3(
                            targetCords.x * gridManager.UnityGridSize,
                            selectedUnit.transform.position.y,
                            targetCords.y * gridManager.UnityGridSize
                        );

                        Debug.Log($"{selectedUnit.name} moved to {targetCords}");

                        selectedUnit.GetComponent<Unit>().MoveUnit(targetCords);
                        selectedUnitCurrentMovementPoints -= manhattanDistance;
                        selectedUnit.GetComponent<Unit>().currentMovementPoints = selectedUnitCurrentMovementPoints;

                        Debug.Log($"Remaining movement points: {selectedUnitCurrentMovementPoints}");
                        HighlightReachableTiles();
                    }
                    else
                    {
                        Debug.Log("Target out of movement range!");
                    }
                }
            }
        }
    }


    public void EnterAttackMode()
    {
        if (selectedUnit != null && selectedUnit.GetComponent<Unit>().numberOfActions > 0)
        {
            isAttacking = true;
            ClearAttackHighlightedTiles();
            ClearHighlightedTiles();
            HighlightAttackRange();
            Debug.Log("Attack Mode");
        }
        else
        {
            Debug.Log("Out of Actions");
        }
    }

    void HighlightReachableTiles()
    {
        if (selectedUnit == null)
        {
            Debug.LogError("No selected unit to highlight movement tiles!");
            return;
        }

        ClearHighlightedTiles();

        Vector2Int startCords = new Vector2Int(
            Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
            Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
        );

        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        frontier.Enqueue(startCords);
        distances[startCords] = 0;

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            int currentDistance = distances[current];

            if (currentDistance < selectedUnitCurrentMovementPoints)
            {
                foreach (Vector2Int direction in new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right })
                {
                    Vector2Int neighbor = current + direction;

                    if (!distances.ContainsKey(neighbor))
                    {
                        GameObject tile = gridManager.GetTileGameObjectAtPosition(neighbor);
                        if (tile != null && !tile.CompareTag("Unwalkable"))
                        {
                            frontier.Enqueue(neighbor);
                            distances[neighbor] = currentDistance + 1;

                            Debug.Log($"Highlighting tile at {neighbor}");

                            MeshRenderer renderer = tile.GetComponentInChildren<MeshRenderer>();
                            if (renderer != null)
                            {
                                renderer.material = highlightMaterial;
                                highlightedTiles.Add(tile);
                            }
                        }
                    }
                }
            }
        }

        Debug.Log($"Total highlighted tiles: {highlightedTiles.Count}");
    }

    public void ClearHighlightedTiles()
    {
        foreach (GameObject tile in highlightedTiles)
        {
            MeshRenderer renderer = tile.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }
        highlightedTiles.Clear();
    }

    void HighlightAttackRange()
    {
        if (selectedUnit == null)
        {
            Debug.LogError("No selected unit to highlight attack range!");
            return;
        }

        ClearAttackHighlightedTiles();

        Vector2Int startCords = new Vector2Int(
            Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
            Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
        );

        int attackRange = selectedUnit.GetComponent<Unit>().attackRange;

        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();

        frontier.Enqueue(startCords);
        distances[startCords] = 0;

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            int currentDistance = distances[current];

            if (currentDistance < attackRange)
            {
                foreach (Vector2Int direction in new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right })
                {
                    Vector2Int neighbor = current + direction;

                    if (!distances.ContainsKey(neighbor))
                    {
                        GameObject tile = gridManager.GetTileGameObjectAtPosition(neighbor);
                        if (tile != null && !highlightedTiles.Contains(tile))
                        {
                            frontier.Enqueue(neighbor);
                            distances[neighbor] = currentDistance + 1;

                            MeshRenderer renderer = tile.GetComponentInChildren<MeshRenderer>();
                            if (renderer != null)
                            {
                                renderer.material = attackHighlightMaterial;
                                attackHighlightedTiles.Add(tile);
                            }
                        }
                    }
                }
            }
        }

        Debug.Log($"Total attack range tiles highlighted: {attackHighlightedTiles.Count}");
    }

    public void ClearAttackHighlightedTiles()
    {
        foreach (GameObject tile in attackHighlightedTiles)
        {
            MeshRenderer renderer = tile.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }
        attackHighlightedTiles.Clear();
    }

    public void OpenConsumablesPanel()
    {
        InventoryUI.Instance.ShowConsumables(UseConsumableOnSelectedUnit);
    }

    public void UseConsumableOnSelectedUnit(NewInventoryItem item)
    {
        if (selectedUnit == null) return;

        Unit unitComponent = selectedUnit.GetComponent<Unit>();
        if (unitComponent == null) return;

        if (item.itemData.itemName == "First Aid Kit")
        {
            unitComponent.health += item.itemData.healAmount;
            if (unitComponent.health > 100) unitComponent.health = 100;

            InventoryManager.Instance.RemoveItem(item.itemData, 1);
            TooltipUI.ShowMessage($"{item.itemData.itemName} used! {selectedUnit.name} healed for {item.itemData.healAmount} HP.");
        }
    }


}
