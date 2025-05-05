using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public List<ItemData> availableItems; // Populate in Inspector
    public int playerCoins = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanAfford(ItemData item)
    {
        return playerCoins >= item.price;
    }

    public void PurchaseItem(ItemData item)
    {
        if (InventoryManager.Instance.SpendCoins(item.price))
        {
            InventoryManager.Instance.AddItem(item, 1); // ✅ Corrected
                                                        // Adjust to your AddItem method signature
            ShopUI.Instance.UpdateCoinDisplay();
            Debug.Log($"Purchased {item.itemName}!");
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item!");
        }
    }

}
