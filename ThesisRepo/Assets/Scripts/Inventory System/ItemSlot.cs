using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemQuantityText;
    public Image itemIcon;

    private bool isFull = false; // Tracks if the slot is occupied

    public void SetItem(string itemName, int quantity, string description)
    {
        itemNameText.text = itemName;
        itemQuantityText.text = "x" + quantity;
        isFull = true;

        // Load the item's sprite dynamically
        ItemData itemData = Resources.Load<ItemData>($"Items/{itemName}");
        if (itemData != null)
        {
            itemIcon.sprite = itemData.icon;
        }
        else
        {
            Debug.LogError($"Icon not found for item: {itemName}");
        }
    }
}

