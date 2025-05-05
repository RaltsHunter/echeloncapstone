using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int price;
    public ItemType itemType;
    public int maxStack = 1; // Maximum stackable amount
    public int healAmount = 0; // Only used if itemType == Consumable


    public enum ItemType
    {
        Consumable,
        Equipment,
        Material,
        Quest
    }
}