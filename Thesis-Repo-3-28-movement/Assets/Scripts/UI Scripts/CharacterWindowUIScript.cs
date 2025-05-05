using UnityEngine;
using UnityEngine.UI; // For UI Text components
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public TMP_Text hpText;  // Reference to the HP text box (child object)
    public TMP_Text mpText;  // Reference to the MP text box (child object)

    [SerializeField] public GameObject unitObject; // Reference to the GameObject that holds the Muscle script
    public Unit unitScript; // Reference to the Muscle script

    void Start()
    {
        // Get the Muscle script from the referenced object
        unitScript = unitObject.GetComponent<Unit>(); // Assuming Muscle script is attached to this object

        if (unitScript == null)
        {
            Debug.LogError("Unit script not found on the object.");
            return;
        }

        // Update the UI for the first time
        UpdateUI();
    }

    void Update()
    {
        // Continuously update the UI if needed (optional based on your needs)
        UpdateUI();
    }

    // Method to update the UI with the current HP and MP values from Muscle script
    void UpdateUI()
    {
        if (unitScript != null)
        {
            // Update HP and MP text
            hpText.text = unitScript.health.ToString();
            //mpText.text = "MP: " + muscleScript.currentMP.ToString();
        }
    }
}
