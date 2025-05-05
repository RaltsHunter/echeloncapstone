using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCombat : MonoBehaviour
{
    public static TriggerCombat Instance { get; private set; }

    public GameObject CombatManagerParent;
    public GameObject ExplorationManager;
    public GameObject combatManager;
    // Start is called before the first frame update

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Remove if you don’t want persistence between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CombatManagerParent = GameObject.Find("Combat Manager Parent");
        combatManager = GameObject.Find("Combat Manager");
        ExplorationManager = GameObject.Find("Exploration Manager");
    }
    void Start()
    {
        
    }

    public void TriggerCombatStart()
    {
        foreach (Transform child in CombatManagerParent.transform)
        {
            child.gameObject.SetActive(true);
            combatManager.GetComponent<CombatManager>().TriggerCombat();

        }

        ExplorationManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
