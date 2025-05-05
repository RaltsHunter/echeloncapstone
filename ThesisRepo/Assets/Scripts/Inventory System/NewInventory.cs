using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    [SerializeField] public List<NewInventoryItem> items = new List<NewInventoryItem>();
    public InventoryUI inventoryUI; // Reference to the UI


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            return;
        }
    }



    public void ToggleInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void AddItem(ItemData itemData, int amount)
    {
        Debug.Log($"Adding {amount} {itemData.itemName} to inventory");
        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);

        if (existingItem != null)
        {
            existingItem.AddQuantity(amount);
        }
        else
        {
            items.Add(new NewInventoryItem(itemData, amount));
        }

        inventoryUI?.RefreshUI(); // Update the UI
    }

    public void RemoveItem(ItemData itemData, int amount)
    {
        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);

        if (existingItem != null)
        {
            existingItem.RemoveQuantity(amount);
            if (existingItem.quantity <= 0)
                items.Remove(existingItem);
        }

        inventoryUI?.RefreshUI(); // Update the UI
    }

    public bool HasItem(ItemData itemData, int amount)
    {
        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);
        return existingItem != null && existingItem.quantity >= amount;
    }
}
