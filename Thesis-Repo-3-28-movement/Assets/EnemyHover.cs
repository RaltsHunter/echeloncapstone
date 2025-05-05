using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    private Unit unit;

    void Start()
    {
       // unit = GetComponent<Unit>();
    }

    void OnMouseEnter()
    {
        if (unit == null)
        {
            unit = GetComponent<Unit>();
        }

        Debug.Log($"OnMouseEnter triggered on {gameObject.name}");

        if (unit != null)
        {
            Debug.Log($"Unit found: {unit.unitName}, HP: {unit.health}");

            if (gameObject.CompareTag("EnemyUnit") && EnemyUIController.Instance != null)
            {
                Debug.Log("Calling ShowEnemyInfo...");
                EnemyUIController.Instance.ShowEnemyInfo(unit.unitName, unit.health);
            }
        }
        else
        {
            Debug.LogWarning("Unit script not found on hovered object!");
        }
    }



    void OnMouseExit()
    {
        if (EnemyUIController.Instance != null)
        {
            EnemyUIController.Instance.HideEnemyInfo();
        }
    }
}