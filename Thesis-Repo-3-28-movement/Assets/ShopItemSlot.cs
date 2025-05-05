using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    public TMP_Text nameText;
    //public TMP_Text priceText;
    public Image iconImage;
    public Button buyButton;

    private ItemData currentItem;

    // Called by ShopUI when the slot is created
    public void Setup(ItemData item)
    {
        
        currentItem = item;
        Debug.Log($"Setting up shop slot for: {item.itemName}");
        // Set visual info
        if (nameText) nameText.text = item.itemName;
        //if (priceText) priceText.text = $"{item.price} Coins";
        if (iconImage) iconImage.sprite = item.icon;

        // Set click behavior
        if (buyButton)
        {
            buyButton.onClick.RemoveAllListeners(); // clear old listeners (important if reused!)
            buyButton.onClick.AddListener(() =>
            {
                ShopManager.Instance.PurchaseItem(currentItem);
            });
        }
    }
}
