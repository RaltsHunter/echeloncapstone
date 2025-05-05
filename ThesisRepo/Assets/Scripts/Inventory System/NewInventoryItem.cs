using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewInventoryItem
{
    public ItemData itemData;
    public int quantity;

    public NewInventoryItem(ItemData data, int amount)
    {
        itemData = data;
        quantity = amount;
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }

    public void RemoveQuantity(int amount)
    {
        quantity -= amount;
        if (quantity < 0) quantity = 0;
    }
}