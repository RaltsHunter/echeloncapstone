using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBoss1 : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        Debug.Log("Secret Boss 1 has died!");
        TooltipUI.ShowMessage("You have defeated the secret boss!");
        Destroy(gameObject);
        // Destroy the boss object
        //StartCoroutine(LeaveAfterDelay(5f));
    }

    public override void DamageRoll()
    {
        Debug.Log("Rolling for damage...");
        this.attackDamage = Random.Range(3, 10);
        TooltipUI.ShowMessage("Secret Boss 1 dealt " + this.attackDamage + " damage!");
    }
}
