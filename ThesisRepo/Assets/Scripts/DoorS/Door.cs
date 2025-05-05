/*using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private float detectionDistance = 5f;  // Detection range
    [SerializeField] private string requiredItemName = "KeyCard1";

    private GameObject[] players;

    private void Update()
    {
        CheckDistanceToPlayers();
    }

    private void CheckDistanceToPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("PlayerUnit");

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= detectionDistance)
            {
                TryUnlockDoor();
                return;  // Stop checking if door is unlocked
            }
        }
    }

    private void TryUnlockDoor()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager != null && PlayerHasKeyCard(inventoryManager))
        {
            Debug.Log("KeyCard1 found! Door unlocked.");
            Destroy(gameObject);  // Unlock the door
        }
        else
        {
            Debug.Log("Door locked. KeyCard1 required.");
        }
    }

    private bool PlayerHasKeyCard(InventoryManager inventoryManager)
    {
        foreach (ItemSlot slot in inventoryManager.itemSlot)
        {
            if (slot.isFull && slot.itemName == requiredItemName)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
*/