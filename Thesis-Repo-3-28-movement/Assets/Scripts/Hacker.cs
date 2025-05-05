using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : Unit
{

    // Start is called before the first frame update

    //Health bar addons
    private CombatManager combatManager;

    [SerializeField] private float maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    void Start()
    {
        combatManager = FindObjectOfType<CombatManager>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Die()
    {
        Debug.Log("Hacker has died!");
        TooltipUI.ShowMessage("Quinn has taken too much damage! You have to flee!");

        if (CombatManager.Instance != null)
        {
            CombatManager.Instance.EndCombat();
        }

        StartCoroutine(LeaveAfterDelay(3f));
    }


    private IEnumerator LeaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LeaveWarehouse.Instance.LeaveBasedOnDay();
    }

    public override void DamageRoll()
    {
        Debug.Log("Rolling for damage...");
        this.attackDamage = Random.Range(1, 6);
        TooltipUI.ShowMessage("Quinn dealt" + this.attackDamage + "damage!");
    }
}
