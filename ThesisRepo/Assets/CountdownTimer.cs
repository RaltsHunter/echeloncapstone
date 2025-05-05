using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the UI Text
    public float timeRemaining = 30f; // 30 seconds countdown
    private bool timerIsActive = false;
    //public SceneManager sceneManager;

    void Start()
    {
        timerText.text = "30"; // Initialize the UI text
        //sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    void Update()
    {
        if (timerIsActive && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeRemaining).ToString(); // Update UI, rounding up
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerText.text = "0"; // Ensure it shows 0 when done
            //sceneManager.FadeAndLoadScene("PlayerRoomA1S3");

            timerIsActive = false;
            Debug.Log(" Timer Ended!");
        }
    }

    // Call this function to start the timer
    public void StartTimer()
    {
        Debug.Log(" Timer Started!");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        timerIsActive = true;
        timeRemaining = 30f; // Reset the timer when starting
    }
}
