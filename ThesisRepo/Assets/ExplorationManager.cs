using UnityEngine;

public class ExplorationManager : MonoBehaviour
{
    public GameObject combatManager; 

/*    void Start()
    {
        
        if (combatManager != null && combatManager.activeSelf)
        {
            gameObject.SetActive(false); // Disable Exploration Mode if Combat is running
        }
    }*/

    void Awake()
    {
        CombatManager foundManager = FindObjectOfType<CombatManager>();
        if (foundManager != null)
        {
            combatManager = foundManager.gameObject;
        }
        else
        {
            Debug.LogError("CombatManager not found in the scene!");
        }


    }

    void Start()
    {
        if (combatManager != null)
        {
            combatManager.SetActive(false); // Ensure Combat Manager is inactive at the start
        }
        else
        {
            Debug.LogError("CombatManager not found in the scene!");
        }
    }

    void OnEnable()
    {
        CombatManager.OnCombatTriggered += DisableExplorationMode;
        CombatManager.OnCombatEnded += EnableExplorationMode;
    }

    void OnDisable()
    {
        CombatManager.OnCombatTriggered -= DisableExplorationMode;
        CombatManager.OnCombatEnded -= EnableExplorationMode;
    }

    public void DisableExplorationMode()
    {
        Debug.Log("Exploration Mode Disabled!");
        gameObject.SetActive(false); 

        if (combatManager)
        {
            foreach (Transform child in combatManager.transform) 
            {
                child.gameObject.SetActive(true);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false); 
        }
    }

    public void EnableExplorationMode()
    {
        Debug.Log("Exploration Mode Enabled!");
        //gameObject.SetActive(true); 
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true); 
        }

        if (combatManager)
        {
            foreach (Transform child in combatManager.transform) 
            {
                child.gameObject.SetActive(false); 
            }
        }
    }
}
