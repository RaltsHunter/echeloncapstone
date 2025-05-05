using UnityEngine;
using TMPro;
using System.Collections;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] public float displayDuration = 3f;

    private Coroutine hideCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
        }
    }

    public static void ShowMessage(string message, float duration = -1f)
    {
        if (Instance != null)
        {
            Instance.DisplayMessage(message, duration);
        }
    }

    private void DisplayMessage(string message, float duration)
    {
        tooltipText.text = message;
        tooltipPanel.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterSeconds(duration > 0 ? duration : displayDuration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        tooltipPanel.SetActive(false);
    }
}
