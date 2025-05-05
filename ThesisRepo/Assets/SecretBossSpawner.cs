using UnityEngine;

public class SecretBossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject secretBossPrefab; // Assign your prefab in the Inspector
    private bool bossSpawned = false;

    void Update()
    {
        if (!bossSpawned && GameManager.Instance != null && GameManager.Instance.totalDocumentCount >= 12)
        {
            Vector3 spawnPosition = new Vector3(3f, 1, 5f);

            GameObject boss = Instantiate(secretBossPrefab, spawnPosition, Quaternion.identity); // ✅ Define the variable here
            bossSpawned = true;

            TooltipUI.ShowMessage(" CALCUTRON EMERGES! ");

            EnemyAI bossAI = boss.GetComponent<EnemyAI>();
            if (bossAI != null)
            {
                bossAI.ManualInitialize(); // ✅ Ensure AI logic starts
            }

            TriggerCombat.Instance?.TriggerCombatStart(); ; // ✅ Start combat
        }
    }
}
