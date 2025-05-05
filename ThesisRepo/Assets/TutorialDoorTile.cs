using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDoorTile : MonoBehaviour
{
    public string requiredItemName;  // The name of the required item
    public float interactionDistance = 0.5f;      // Distance to player to allow interaction
    public float doorDestroyRadius = 0.5f;        // Radius to search for WarehouseDoor prefab

    private GameObject hacker;

    void Start()
    {
        

        if (hacker == null)
        {
            Debug.LogError("GameObject named 'Hacker' not found in the scene!");
        }
    }

    void Update()
    {
        hacker = GameObject.Find("Hacker");
        if (hacker == null) return;

        // Only the closest tile to Hacker is allowed to interact
        if (!IsThisTheClosestTileTo(hacker)) return;

        Vector3 doorPoint = GetClosestEdge(gameObject, hacker);
        Vector3 hackerPoint = GetClosestEdge(hacker, gameObject);

        float distance = Vector3.Distance(doorPoint, hackerPoint);
        Debug.Log($"Distance to Hacker: {distance}");

        Debug.DrawLine(hackerPoint, doorPoint, Color.blue);

        if (distance <= interactionDistance){
            TooltipUI.ShowMessage("Press F to unlock... if you have the key");
        }

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interacting with door tile.");
            TryUnlockDoor();
        }
    }

    bool IsThisTheClosestTileTo(GameObject player)
    {
        DoorTileScript[] allTiles = FindObjectsOfType<DoorTileScript>();
        float thisDistance = Vector3.Distance(
            GetClosestEdge(gameObject, player),
            GetClosestEdge(player, gameObject)
        );

        foreach (var tile in allTiles)
        {
            if (tile == this) continue;

            float otherDistance = Vector3.Distance(
                GetClosestEdge(tile.gameObject, player),
                GetClosestEdge(player, tile.gameObject)
            );

            if (otherDistance < thisDistance)
            {
                return false; // Another tile is closer to the Hacker
            }
        }

        return true;
    }

    void TryUnlockDoor()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager instance not found!");
            return;
        }

        foreach (var item in InventoryManager.Instance.items)
        {
            if (item.itemData.itemName == requiredItemName && item.quantity > 0)
            {
                Debug.Log("Correct keycard found! Unlocking door tile.");

                gameObject.tag = "Tile"; // Unlock the tile visually/logically
                DestroyNearbyWarehouseDoor();

                // Scene loading is commented out but available if needed
                UnityEngine.SceneManagement.SceneManager.LoadScene("WH Main 2");
                return;
            }
        }

        Debug.Log("Missing required item: " + requiredItemName);
    }

    void DestroyNearbyWarehouseDoor()
    {
        GameObject[] allDoors = GameObject.FindGameObjectsWithTag("WarehouseDoor");

        foreach (GameObject door in allDoors)
        {
            float distance = Vector3.Distance(
                GetClosestEdge(gameObject, door),
                GetClosestEdge(door, gameObject)
            );

            if (distance <= doorDestroyRadius)
            {
                Debug.Log("WarehouseDoor found and destroyed.");
                Destroy(door);
                return;
            }
        }

        Debug.LogWarning("No WarehouseDoor found within range.");
    }

    Vector3 GetClosestEdge(GameObject source, GameObject target)
    {
        Collider sourceCollider = source.GetComponentInChildren<Collider>();
        if (sourceCollider != null)
        {
            return sourceCollider.ClosestPoint(target.transform.position);
        }
        return source.transform.position;
    }
}
