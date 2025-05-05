using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCombat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CombatManagerParent;
    void Start()
    {
        CombatManagerParent = GameObject.Find("Combat Manager Parent");
        if (CombatManagerParent != null)
        {
            foreach (Transform child in CombatManagerParent.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Combat Manager Parent not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
