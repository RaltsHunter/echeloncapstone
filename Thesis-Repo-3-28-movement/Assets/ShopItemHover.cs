using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ShopTooltip shopTooltip;
    public string itemDescription;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (shopTooltip != null)
            shopTooltip.ShowTooltip(itemDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (shopTooltip != null)
            shopTooltip.HideTooltip();
    }
}
