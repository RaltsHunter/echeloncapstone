using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueTriggerOnce : MonoBehaviour
{
    public string conversationName = "NPC_Intro";
    private CountdownTimer countdownTimer; // Reference to CountdownTimer

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            // Find the TimerController GameObject
            GameObject alarm = GameObject.Find("TimerController");

            if (alarm != null)
            {
                countdownTimer = alarm.GetComponent<CountdownTimer>();

                if (countdownTimer != null)
                {
                    countdownTimer.StartTimer(); // ✅ Call StartTimer() correctly
                }
                else
                {
                    Debug.LogError("CountdownTimer component not found on TimerController!");
                }
            }
            else
            {
                Debug.LogError("TimerController GameObject not found!");
            }

            // Disable this trigger so it doesn't repeat
            gameObject.SetActive(false);
        }
    }
}
