using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveHub : MonoBehaviour
{
    public GameObject leaveWarehouseCanvas; // Assign in inspector
    private GameObject explorationManager;
    private GameObject combatManagerParent;

    private void Awake()
    {
        explorationManager = GameObject.Find("Exploration Manager");
        combatManagerParent = GameObject.Find("Combat Manager Parent");
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
                GameManager.Instance.currentDay = 2;
                if (explorationManager != null) Destroy(explorationManager);
                if (combatManagerParent != null) Destroy(combatManagerParent);
                SceneManager.LoadScene("outsideWarehouse 2");
                break;

            case 2:
                GameManager.Instance.currentDay = 3;
                if (explorationManager != null) Destroy(explorationManager);
                if (combatManagerParent != null) Destroy(combatManagerParent);
                SceneManager.LoadScene("outsideWarehouse3");
                break;

            case 3:
                SceneManager.LoadScene("PlayerRoomA3S3");
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
