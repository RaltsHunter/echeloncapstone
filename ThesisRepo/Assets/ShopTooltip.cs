using UnityEngine;
using TMPro;

public class ShopTooltip : MonoBehaviour
{
    public TMP_Text tooltipText;

    void Start()
    {
        gameObject.SetActive(false); // Start hidden
    }

    public void ShowTooltip(string description)
    {
        tooltipText.text = description;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
