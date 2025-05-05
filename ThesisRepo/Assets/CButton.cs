using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CButton : MonoBehaviour
{
    private NewInventoryItem item;
    private UnitController unitController;
    private ConsumableUIManager manager;

    [SerializeField] private TMP_Text label;

    public void Initialize(NewInventoryItem item, UnitController controller, ConsumableUIManager uiManager)
    {
        this.item = item;
        this.unitController = controller;
        this.manager = uiManager;

        if (label != null)
        {
            label.text = $"{item.itemData.itemName}";
        }

        GetComponent<Button>().onClick.RemoveAllListeners(); // Avoid duplicates
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (unitController != null && item != null)
        {
            unitController.UseConsumableOnSelectedUnit(item);
        }

        if (manager != null)
        {
            manager.ToggleConsumablesPanel(); // Hide panel after use
        }
    }
}
