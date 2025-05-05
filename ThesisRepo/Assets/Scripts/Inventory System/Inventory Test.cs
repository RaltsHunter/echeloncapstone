using UnityEngine;

public class InventoryTest : MonoBehaviour
{

    public Inventory inventory;
    public ItemData testItem; // Assign an ItemData in the Inspector


   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            inventory.AddItem(testItem, 1);
            Debug.Log($"Added {testItem.itemName}. Current quantity: {GetItemQuantity()}");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.RemoveItem(testItem, 1);
            Debug.Log($"Removed {testItem.itemName}. Current quantity: {GetItemQuantity()}");
        }
    }

    int GetItemQuantity()
    {
        var item = inventory.items.Find(i => i.itemData == testItem);
        return item != null ? item.quantity : 0;
    }
}