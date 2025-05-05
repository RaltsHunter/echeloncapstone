using System; 
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ShopInventoryUI : MonoBehaviour
{
    public Inventory inventory; // Reference to player inventory
    public GameObject slotPrefab; // Inventory slot prefab
    public Transform inventoryContainer; // The UI container for slots

    private List<GameObject> inventorySlots = new List<GameObject>();

    void Start()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        // Clear previous slots
        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();

        // Generate new slots for each inventory item
        foreach (NewInventoryItem item in inventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryContainer);
            inventorySlots.Add(slot);

            // Set the item icon
            Image icon = slot.GetComponentInChildren<Image>();
            if (icon) icon.sprite = item.itemData.icon;

            // Set the item name and quantity
            TMP_Text text = slot.GetComponentInChildren<TMP_Text>();
            if (text) text.text = $"{item.itemData.itemName} x{item.quantity}";
        }
    }
}
