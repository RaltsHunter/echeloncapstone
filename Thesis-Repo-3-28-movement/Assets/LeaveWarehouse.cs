using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveWarehouse : MonoBehaviour
{
    public static LeaveWarehouse Instance { get; private set; }

    public GameObject leaveWarehouseCanvas; // Assign in inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optional: Uncomment if you want it to persist between scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log("Player entered trigger zone.");
            if (leaveWarehouseCanvas != null)
            {
                leaveWarehouseCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log("Player exited trigger zone.");
            if (leaveWarehouseCanvas != null)
            {
                leaveWarehouseCanvas.SetActive(false);
            }
        }
    }

    public void LeaveBasedOnDay()
    {
        int currentDay = GameManager.Instance.currentDay;

        switch (currentDay)
        {
            case 1:
                SceneManager.LoadScene("PlayerRoomA1S3");
                break;
            case 2:
                SceneManager.LoadScene("PlayerRoomA1S4");
                break;
            case 3:
                SceneManager.LoadScene("PlayerRoomA1S5");
                break;
            default:
                Debug.LogWarning("Unhandled day value in GameManager!");
                break;
        }
    }

    public void DontLeave()
    {
        if (leaveWarehouseCanvas != null)
        {
            leaveWarehouseCanvas.SetActive(false);
        }
    }
}
