using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{
    public GameObject enemyUIPanel;
    public TMP_Text enemyNameText;
    public TMP_Text enemyHPText;

    public static EnemyUIController Instance { get; private set; }

    private bool isShowing = false;
    private Vector2 offset = new Vector2(20, -20); // Offset from mouse position

    void Awake()
    {
        Instance = this;

        if (enemyUIPanel != null)
        {
            enemyUIPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Enemy UI Panel not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (isShowing)
        {
            FollowMouse();
        }
    }

    public void ShowEnemyInfo(string enemyName, int hp)
    {
        if (enemyUIPanel == null) return;

        enemyNameText.text = enemyName;
        enemyHPText.text = "HP: " + hp;
        enemyUIPanel.SetActive(true);
        isShowing = true;
        FollowMouse(); // Set initial position
    }

    public void HideEnemyInfo()
    {
        if (enemyUIPanel == null) return;

        enemyUIPanel.SetActive(false);
        isShowing = false;
    }

    private void FollowMouse()
    {
        RectTransform parentRect = enemyUIPanel.transform.parent.GetComponent<RectTransform>();
        RectTransform panelRect = enemyUIPanel.GetComponent<RectTransform>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            Input.mousePosition,
            null, // Assuming Screen Space - Overlay canvas
            out localPoint
        );

        panelRect.anchoredPosition = localPoint + offset;
    }
}
