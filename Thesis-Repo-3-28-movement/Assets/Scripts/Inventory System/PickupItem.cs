using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PickupItem : MonoBehaviour
{
    public ItemData itemData; // Assigned by LootDrop or Inspector
    public int quantity = 1;  // Default quantity

    private bool openedInventoryBefore = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerUnit")) return;

        if (itemData == null)
        {
            Debug.LogWarning("PickupItem has no ItemData assigned.");
            return;
        }

        Debug.Log($"Player picked up: {itemData.itemName} x{quantity}");

        string message = $"{itemData.itemName} x{quantity} added to inventory.";
        if (!openedInventoryBefore)
        {
            openedInventoryBefore = true;
            message += " Press I to open it.";
        }

        TooltipUI.ShowMessage(message);

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemData, quantity);

            if (itemData.itemType == ItemData.ItemType.Quest)
            {
                GameManager.Instance?.AddDocument();
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("InventoryManager.Instance not found!");
        }
    }
}
