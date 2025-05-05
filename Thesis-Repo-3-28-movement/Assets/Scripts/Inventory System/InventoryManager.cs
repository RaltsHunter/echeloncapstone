using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] public List<NewInventoryItem> items = new List<NewInventoryItem>();
    public GameObject inventoryCanvas;
    public Transform itemContainer;
    public GameObject itemSlotPrefab;
    public int playerCoins = 10;

    private bool menuActivated = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (inventoryCanvas != null)
        {
            //inventoryCanvas.SetActive(false);
        }
    }

    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.I))
        {
            menuActivated = !menuActivated;
            if (inventoryCanvas != null)
            {
                inventoryCanvas.SetActive(menuActivated);
            }
            InventoryUI.Instance?.ToggleInventory(); // ✅ Correct

        }*/
    }



    public void AddItem(ItemData itemData, int quantity)
    {
        Debug.Log($"Adding {quantity}x {itemData.itemName} to inventory");

        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);
        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            items.Add(new NewInventoryItem(itemData, quantity));
        }

        InventoryUI.Instance?.RefreshUI();
    }

    public void RemoveItem(ItemData itemData, int amount)
    {
        NewInventoryItem existingItem = items.Find(i => i.itemData == itemData);

        if (existingItem != null)
        {
            existingItem.RemoveQuantity(amount);
            Debug.Log($"Removed {amount} {itemData.itemName}, Remaining: {existingItem.quantity}");

            if (existingItem.quantity <= 0)
            {
                Debug.Log($"{itemData.itemName} fully removed from inventory.");
                items.Remove(existingItem);
            }
        }
        else
        {
            Debug.LogWarning($"Tried to remove {itemData.itemName}, but it wasn't found in inventory.");
        }

        InventoryUI.Instance?.RefreshUI();
    }

    public bool SpendCoins(int amount)
    {
        if (playerCoins >= amount)
        {
            playerCoins -= amount;
            return true;
        }
        return false;
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
    }
}
