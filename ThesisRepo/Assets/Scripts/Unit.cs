using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public int movementPointsPerTurn = 3;
    public int currentMovementPoints;
    [SerializeField] private bool isPlayer;
    public int attackRange = 1;
    public int attackDamage = 1;
    public int health = 10;
    public GameObject target;
    [SerializeField] public GameObject unitController;
    public int numberOfActions = 1;
    [SerializeField] public string unitName;
    
    

    private void Start()
    {
        ResetMovementPoints();
        unitController = GameObject.Find("UnitController");
    }

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");

        // ✅ Drop loot if component is attached
        LootDrop loot = GetComponent<LootDrop>();
        if (loot != null)
        {
            loot.DropLoot();
        }

        Destroy(gameObject);
    }


    public void Attack(Unit target)
    {
        if (target == null)
        {
            Debug.LogWarning($"{gameObject.name} tried to attack, but target was null.");
            unitController.GetComponent<UnitController>().isAttacking = false;
            return;
        }

        Vector2Int currentPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        Vector2Int targetPosition = new Vector2Int(Mathf.RoundToInt(target.transform.position.x), Mathf.RoundToInt(target.transform.position.z));

        // Calculate Manhattan distance
        int distance = Mathf.Abs(currentPosition.x - targetPosition.x) + Mathf.Abs(currentPosition.y - targetPosition.y);

        if (distance <= attackRange)
        {
            DamageRoll();

            // Double check target isn't already destroyed
            if (target != null)
            {
                target.TakeDamage(attackDamage);
                Debug.Log($"{gameObject.name} attacked {target.gameObject.name} for {attackDamage} damage.");

                // ✅ Only update UI if target is still alive and tagged correctly
                if (EnemyUIController.Instance != null && target.health > 0 && target.CompareTag("EnemyUnit"))
                {
                    EnemyUIController.Instance.ShowEnemyInfo(target.unitName, target.health);
                }
            }

            if (unitController != null)
            {
                UnitController controller = unitController.GetComponent<UnitController>();
                if (controller != null)
                {
                    controller.isAttacking = false;
                }
            }

            numberOfActions--;
        }
        else
        {
            Debug.Log($"{gameObject.name} cannot attack {target.gameObject.name} because it is out of range.");

            if (unitController != null)
            {
                UnitController controller = unitController.GetComponent<UnitController>();
                if (controller != null)
                {
                    controller.isAttacking = false;
                }
            }
        }
    }


    public void ResetMovementPoints()
    {
        currentMovementPoints = movementPointsPerTurn;
    }

    public bool HasMovementPoints()
    {
        return currentMovementPoints > 0;
    }

    public void MoveUnit(Vector2Int targetPosition)
    {
        if (HasMovementPoints())
        {
            // Implement movement logic here (e.g., updating position)
            currentMovementPoints--;

            Debug.Log($"{gameObject.name} moved to {targetPosition}. Remaining movement points: {currentMovementPoints}");
        }
        else
        {
            Debug.Log($"{gameObject.name} has no movement points left.");
        }
    }

    public int GetRemainingMovementPoints()
    {
        return currentMovementPoints;
    }

    public virtual void DamageRoll()
    {
        Debug.Log("Rolling for damage..."); 
        this.attackDamage = Random.Range(1, 6);
    }

    public void ResetActions()
    {
        numberOfActions = 1;
    }
}
