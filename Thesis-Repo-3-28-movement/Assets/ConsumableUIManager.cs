using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ConsumableUIManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject panel;
    [SerializeField] private UnitController unitController;

    public void ToggleConsumablesPanel()
    {
        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
        {
            RefreshConsumableButtons();
        }
    }

    private void RefreshConsumableButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (NewInventoryItem item in InventoryManager.Instance.items)
        {
            if (item.itemData.itemType == ItemData.ItemType.Consumable)
            {
                GameObject buttonGO = Instantiate(buttonPrefab, buttonContainer);
                CButton cb = buttonGO.GetComponent<CButton>();
                if (cb != null)
                {
                    cb.Initialize(item, unitController, this);
                }
            }
        }
    }

}
