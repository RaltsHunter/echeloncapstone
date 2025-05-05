using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsATrap : MonoBehaviour
{
    public GameObject CombatManager;
    private CombatManager combatManagerScript; // ✅ Corrected reference

    private void Start()
    {
        CombatManager = GameObject.Find("Combat Manager");

        if (CombatManager != null)
        {
            combatManagerScript = CombatManager.GetComponent<CombatManager>();

            if (combatManagerScript == null)
            {
                Debug.LogError("CombatManager script is missing on Combat Manager GameObject!");
            }
        }
        else
        {
            combatManagerScript.TriggerCombat();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            if (combatManagerScript != null)
            {
                combatManagerScript.TriggerCombat(); // ✅ Now calls CombatManager
            }
            else
            {
                Debug.LogError("combatManagerScript is null, cannot start combat!");
            }
        }

        gameObject.SetActive(false); // ✅ Disable the trap after triggering
    }
}
