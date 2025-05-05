using UnityEngine;
using TMPro;

public class InventorySlotScript : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;

    // Set item name on this slot
    public void SetItemName(string name)
    {
        itemNameText = GetComponentInChildren<TMP_Text>();
        if (itemNameText != null)
        {
            itemNameText.text = name;
        }
        else
        {
            Debug.LogWarning("ItemNameText reference not set on ItemSlot.");
        }
    }
}