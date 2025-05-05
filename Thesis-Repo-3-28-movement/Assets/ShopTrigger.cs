using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopCanvasManager; // Assign in Inspector
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && shopCanvasManager != null)
        {
            bool anyActive = false;

            // Check if any child is active
            foreach (Transform child in shopCanvasManager.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    anyActive = true;
                    break;
                }
            }

            // Toggle all children
            foreach (Transform child in shopCanvasManager.transform)
            {
                child.gameObject.SetActive(!anyActive);
            }

            Debug.Log($"Shop UI toggled: {(anyActive ? "Hidden" : "Shown")}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit")) // Make sure your player is tagged correctly
        {
            playerInRange = true;
            TooltipUI.ShowMessage("Open the shop with F.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            playerInRange = false;
            foreach (Transform child in shopCanvasManager.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
