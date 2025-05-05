using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        CombatManager.OnCombatTriggered += ShowCombatUI;
    }

    void OnDisable()
    {
        CombatManager.OnCombatTriggered -= ShowCombatUI;
    }

    public void ShowCombatUI()
    {
        Debug.Log("Combat UI Activated!");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
