/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class InventoryUIUpdater : MonoBehaviour
{
    public TMP_Text[] itemTextBoxes; // Array of pre-existing text boxes to display item information
    private InventoryManager inventoryManager; // Reference to the InventoryManager

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found.");
            return;
        }

        // Initial display of inventory
        UpdateInventoryUI();
    }

    // Call this to update the inventory UI
    public void UpdateInventoryUI()
    {
        // Check if the number of items exceeds the available text boxes
        int numItemsToDisplay = Mathf.Min(inventoryManager.inventory.Count, itemTextBoxes.Length);

        // Clear the existing text boxes
        for (int i = 0; i < itemTextBoxes.Length; i++)
        {
            itemTextBoxes[i].text = ""; // Clear text
        }

        // Loop through the inventory and fill the text boxes
        for (int i = 0; i < numItemsToDisplay; i++)
        {
            Item item = inventoryManager.inventory[i];
            itemTextBoxes[i].text = $"{item.itemName} (x{item.quantity})"; // Set item name and quantity
        }
    }
}
*/