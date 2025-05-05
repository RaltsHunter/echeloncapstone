using UnityEngine;
using TMPro;
using System.Collections;

public class SecurityPadIndicator : MonoBehaviour
{
    [SerializeField] private Renderer padRenderer;
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color combatColor = Color.red;

    [Header("Countdown Settings")]
    [SerializeField] private float countdownTime = 60f;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject countdownUI;

    private Coroutine countdownRoutine;
    private bool playerInRange = false;
    private bool countdownActive = false;

    private void Start()
    {
        if (padRenderer == null)
            padRenderer = GetComponent<Renderer>();

        padRenderer.material.color = FindObjectOfType<CombatManager>()?.combatActive == true
            ? combatColor
            : safeColor;

        if (countdownUI != null)
            countdownUI.SetActive(false);
    }

    private void OnEnable()
    {
        CombatManager.OnCombatTriggered += SetColorCombat;
        CombatManager.OnCombatEnded += StartCountdown;
    }

    private void OnDisable()
    {
        CombatManager.OnCombatTriggered -= SetColorCombat;
        CombatManager.OnCombatEnded -= StartCountdown;
    }

    private void Update()
    {
        if (playerInRange && countdownActive && Input.GetKeyDown(KeyCode.F))
        {
            DisarmSecurityPad();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit") && countdownActive)
        {
            playerInRange = true;
            TooltipUI.ShowMessage("Press F to disarm");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            playerInRange = false;
        }
    }

    private void SetColorCombat()
    {
        padRenderer.material.color = combatColor;

        if (countdownUI != null)
            countdownUI.SetActive(false);

        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);
    }

    private void SetColorSafe()
    {
        padRenderer.material.color = safeColor;
    }

    private void StartCountdown()
    {
        if (countdownRoutine != null)
            StopCoroutine(countdownRoutine);

        countdownRoutine = StartCoroutine(CountdownTimer());
        countdownActive = true;

        TooltipUI.Instance.displayDuration = 5f;
        TooltipUI.ShowMessage("Fighting triggered the alarm! Get Quinn to a security terminal in 60 seconds or you will have to flee!");
        TooltipUI.Instance.displayDuration = 3f;
    }

    private IEnumerator CountdownTimer()
    {
        float timeLeft = countdownTime;

        if (countdownUI != null)
            countdownUI.SetActive(true);

        while (timeLeft > 0f)
        {
            if (countdownText != null)
                countdownText.text = $"Lockdown in: {Mathf.CeilToInt(timeLeft)}s";

            timeLeft -= 1f;
            yield return new WaitForSeconds(1f);
        }

        if (countdownText != null)
            countdownText.text = "⚠️ LOCKDOWN INITIATED ⚠️";

        Debug.Log("Security lockdown has been triggered!");
        countdownActive = false;
        // Add additional fail logic here
    }

    private void DisarmSecurityPad()
    {
        Debug.Log("Security pad disarmed by player.");
        if (countdownRoutine != null)
        {
            StopCoroutine(countdownRoutine);
            countdownRoutine = null;
        }

        if (countdownText != null)
            countdownText.text = "Security Disarmed";

        if (countdownUI != null)
            countdownUI.SetActive(false);

        countdownActive = false;
        SetColorSafe();
    }
}
