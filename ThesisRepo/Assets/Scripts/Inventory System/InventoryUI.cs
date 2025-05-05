using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public InventoryManager inventory;
    public GameObject slotPrefab;
    public Transform itemContainer;
    public GameObject inventoryPanel;

    // ✅ New UI elements for item details
    public GameObject itemDetailsPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    private List<GameObject> slots = new List<GameObject>();

    void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Duplicate InventoryUI found, destroying...");
            Destroy(gameObject);
            return;
        }*/

        Debug.Log("InventoryUI Instance is set correctly.");
    }

    void Start()
    {
        Debug.Log("InventoryUI Start() running...");

        GameObject inventoryManagerObj = GameObject.Find("InventoryManager");
        if (inventoryManagerObj == null)
        {
            Debug.LogError("InventoryManager not found in the scene!");
            return;
        }

        inventory = inventoryManagerObj.GetComponent<InventoryManager>();

        if (inventory == null)
        {
            Debug.LogError("InventoryManager script is missing on the InventoryManager object!");
            return;
        }

        Debug.Log("InventoryManager successfully found and assigned.");

        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is not assigned in the Inspector!");
            return;
        }

        inventoryPanel.SetActive(false);
        itemDetailsPanel.SetActive(false);
        RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I key pressed!");

            //inventory = InventoryManager.Instance; // ✅ Moved here BEFORE ToggleInventory()

            ToggleInventory();
            RefreshUI();
        }
    }


    public void ToggleInventory()
    {
        Debug.Log("ToggleInventory() called!");

        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is missing!");
            return;
        }

        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);
        itemDetailsPanel.SetActive(!isActive);

        Debug.Log($"InventoryPanel is now {(inventoryPanel.activeSelf ? "ON" : "OFF")}");

        if (!isActive) // When opening inventory
        {
            Debug.Log("Calling RefreshUI() from ToggleInventory()...");

            if (inventory == null)
            {
                Debug.LogError("Inventory reference is NULL when trying to refresh UI!");
                return;
            }

            RefreshUI(); // ✅ Should run now
        }
    }

    public void RefreshUI()
    {
        Debug.Log(" RefreshUI() called...");

        if (inventory == null)
        {
            Debug.LogError("❌ Inventory reference is missing!");
            return;
        }

        Debug.Log($" Refreshing Inventory UI... Inventory contains {inventory.items.Count} items.");

        // ✅ Check Before Clearing Slots
        Debug.Log($"Clearing {slots.Count} existing slots.");

        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();

        if (inventory.items.Count == 0)
        {
            Debug.LogWarning("Inventory is empty, no slots to display.");
        }

        foreach (NewInventoryItem item in inventory.items)
        {
            Debug.Log($" Creating slot for {item.itemData.itemName}, Quantity: {item.quantity}");

            GameObject slot = Instantiate(slotPrefab, itemContainer);
            slots.Add(slot);

            Image icon = slot.GetComponentInChildren<Image>();

            InventorySlotScript itemSlot = slot.GetComponent<InventorySlotScript>();
            if (itemSlot != null)
            {
                itemSlot.SetItemName(item.itemData.itemName);
            }
            if (icon)
            {
                icon.sprite = item.itemData.icon;
            }

            Button button = slot.GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(() => ShowItemDetails(item));
            }
        }

        Debug.Log($"Finished refreshing UI. Total slots: {slots.Count}");
    }


   /* void UseItem(NewInventoryItem item)
    {
        Debug.Log($"🛠️ Using {item.itemData.itemName}");

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.RemoveItem(item.itemData, 1);
        }
        else
        {
            Debug.LogError("❌ InventoryManager instance not found!");
        }

        RefreshUI(); // ✅ Ensure UI updates after using an item
    }*/



    void ShowItemDetails(NewInventoryItem item)
    {
        Debug.Log($"Showing details for {item.itemData.itemName}");

        if (itemDetailsPanel == null)
        {
            Debug.LogError("Item Details Panel is missing!");
            return;
        }

        itemNameText.text = item.itemData.itemName;
        itemDescriptionText.text = item.itemData.description;

        itemDetailsPanel.SetActive(true);
    }

    public GameObject consumableSlotPrefab;
    public Transform consumableContainer;
    public GameObject consumablePanel;

    public void ShowConsumables(System.Action<NewInventoryItem> onItemUsed)
    {
        foreach (Transform child in consumableContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in inventory.items)
        {
            if (item.itemData.itemType == ItemData.ItemType.Consumable)
            {
                GameObject slot = Instantiate(consumableSlotPrefab, consumableContainer);
                slot.GetComponentInChildren<TMP_Text>().text = item.itemData.itemName;

                Button button = slot.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        onItemUsed?.Invoke(item);
                        consumablePanel.SetActive(false); // Close after use
                    });
                }
            }
        }

        consumablePanel.SetActive(true);
    }

}
