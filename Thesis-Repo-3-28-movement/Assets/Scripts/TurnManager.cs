using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public TurnState currentState;

    [SerializeField] private List<Unit> playerUnits; // Reference to player units
    [SerializeField] private List<Unit> enemyUnits;  // Reference to enemy units
    [SerializeField] public int roundCounter = 20;
    [SerializeField] private TMP_Text roundCounterText;
    private CombatManager combatManager;


    void Start()
    {
        playerUnits = new List<Unit>();
        UpdateRoundUI();
        GameObject[] playerUnitsGameObjects = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject unitObj in playerUnitsGameObjects)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            if (unit != null)
            {
                playerUnits.Add(unit);
            }
        }

        combatManager = FindObjectOfType<CombatManager>();
        if (combatManager == null)
        {
            Debug.LogError("CombatManager not found!");
        }

        enemyUnits = new List<Unit>();

        GameObject[] enemyUnitsGameObjects = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject unitObj in enemyUnitsGameObjects)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            EnemyAI ai = unitObj.GetComponent<EnemyAI>();
            if (unit != null && ai != null && ai.IsNearPlayerForCombat())
            {
                enemyUnits.Add(unit);
            }
        }

        currentState = TurnState.PlayerTurn;
        ResetPlayerUnits();
        StartPlayerTurn();
    }

    private void RefreshEnemyUnits()
    {
        enemyUnits.Clear();

        GameObject[] enemyUnitsGameObjects = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject unitObj in enemyUnitsGameObjects)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            EnemyAI ai = unitObj.GetComponent<EnemyAI>();
            if (unit != null && ai != null && ai.IsNearPlayerForCombat())
            {
                enemyUnits.Add(unit);
            }
        }
    }


    void Update()
    {
        if (currentState == TurnState.PlayerTurn && AllPlayerUnitsOutOfMovement())
        {
            EndPlayerTurn();
        }
    }

    private bool AllPlayerUnitsOutOfMovement()
    {
        foreach (Unit unit in playerUnits)
        {
            if (unit.HasMovementPoints())
            {
                return false;
            }
        }
        return true; // All player units are out of movement points
    }



    public void EndPlayerTurn()
    {
        Debug.Log("Player turn has ended.");
        currentState = TurnState.EnemyTurn;
        ResetEnemyUnits();
        StartCoroutine(StartEnemyTurn());
    }

    private IEnumerator StartEnemyTurn()
    {
        Debug.Log("Enemy turn has started.");

        RefreshEnemyUnits(); // ✅ Add this here!

        List<Coroutine> enemyCoroutines = new List<Coroutine>();

        foreach (Unit enemy in enemyUnits)
        {
            if (enemy != null)
            {
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyCoroutines.Add(StartCoroutine(enemyAI.EnemyTurn()));
                }
                else
                {
                    Debug.LogWarning($"EnemyAI component not found on {enemy.name}");
                }
            }
            else
            {
                Debug.LogWarning("An enemy unit has been destroyed, skipping.");
            }
        }

        foreach (Coroutine coroutine in enemyCoroutines)
        {
            yield return coroutine;
        }

        EndEnemyTurn();
    }



    public void EndEnemyTurn()
    {
        Debug.Log("Enemy turn has ended.");

        roundCounter--;
        Debug.Log($"Round {20 - roundCounter} has ended. {roundCounter} rounds left.");

        UpdateRoundUI();

        // ✅ End combat if no enemy units are left
        enemyUnits.RemoveAll(unit => unit == null); // Clean up destroyed units
        if (enemyUnits.Count == 0)
        {
            Debug.Log("No enemies remaining. Ending combat.");
            if (combatManager != null)
            {
                combatManager.RequestCombatEnd();

            }
            return;
        }

        if (roundCounter <= 0)
        {
            Debug.Log("Game Over!");
            return;
        }

        currentState = TurnState.PlayerTurn;
        ResetPlayerUnits();
        StartPlayerTurn();
    }


    private void UpdateRoundUI()
    {
        if (roundCounterText != null)
        {
            roundCounterText.text = $"Rounds Remaining: {roundCounter}";
        }
        else
        {
            Debug.LogWarning("RoundCounterText is not assigned in the Inspector.");
        }
    }


    private void StartPlayerTurn()
    {
        Debug.Log("Player turn has started.");
        // Enable player controls, UI updates, etc.
    }

    private void ResetPlayerUnits()
    {
        foreach (Unit unit in playerUnits)
        {
            unit.ResetMovementPoints();
            unit.ResetActions();
        }
    }

    private void ResetEnemyUnits()
    {
        foreach (Unit unit in enemyUnits)
        {
            unit.ResetMovementPoints();
        }
    }
}