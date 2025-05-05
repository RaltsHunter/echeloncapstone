using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [Header("Shop Item UI")]
    public GameObject shopItemPrefab;
    public Transform shopItemContainer;
    public TMP_Text coinText;

    [Header("Tooltip Panel")]
    public GameObject tooltipPanel;
    public TMP_Text tooltipItemNameText;
    public TMP_Text tooltipItemDescriptionText;
    public Button confirmBuyButton;

    private ItemData currentlySelectedItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshShopUI();
        UpdateCoinDisplay();
        tooltipPanel.SetActive(false);
    }

    public void RefreshShopUI()
    {
        foreach (Transform child in shopItemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData item in ShopManager.Instance.availableItems)
        {
            GameObject slot = Instantiate(shopItemPrefab, shopItemContainer);
            ShopItemSlot shopItemSlot = slot.GetComponent<ShopItemSlot>();

            if (shopItemSlot != null)
            {
                shopItemSlot.Setup(item);

                // Hook up tooltip on click
                Button button = shopItemSlot.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => ShowTooltip(item));
                }
            }
        }

        coinText.text = $"Dollars: {ShopManager.Instance.playerCoins}";
    }

    public void ShowTooltip(ItemData item)
    {
        currentlySelectedItem = item;

        tooltipItemNameText.text = item.itemName;
        tooltipItemDescriptionText.text =
            $"{item.description}\n\nPrice: {item.price} dollars";

        tooltipPanel.SetActive(true);

        confirmBuyButton.onClick.RemoveAllListeners();
        confirmBuyButton.onClick.AddListener(BuySelectedItem);
    }

    private void BuySelectedItem()
    {
        if (currentlySelectedItem == null) return;

        if (ShopManager.Instance.playerCoins >= currentlySelectedItem.price)
        {
            //InventoryManager.Instance.AddItem(currentlySelectedItem);
            ShopManager.Instance.playerCoins -= currentlySelectedItem.price;

            Debug.Log($"Purchased: {currentlySelectedItem.itemName}");
            RefreshShopUI();
            tooltipPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"Dollars: {InventoryManager.Instance.playerCoins}";
        }
    }
}
