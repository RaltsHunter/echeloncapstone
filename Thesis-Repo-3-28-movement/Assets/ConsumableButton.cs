using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsumableButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    private NewInventoryItem item;
    private UnitController unitController;

    public void Initialize(NewInventoryItem newItem, UnitController controller)
    {
        item = newItem;
        unitController = controller;
        label.text = $"{item.itemData.itemName} x{item.quantity}";
    }

    public void OnClick()
    {
        //ConsumablesUIManager.Instance.ShowConsumables(selectedUnit);

    }
}
