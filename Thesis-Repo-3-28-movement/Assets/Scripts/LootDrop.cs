using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField] private GameObject lootPickupPrefab; // Prefab with a PickupItem + collider
    [SerializeField] private ItemData itemToDrop;
    [SerializeField] private int quantity = 1;

    public void DropLoot()
    {
        if (lootPickupPrefab == null || itemToDrop == null) return;

        Vector3 dropPosition = transform.position + Vector3.up * 0.5f; // slight offset
        GameObject loot = Instantiate(lootPickupPrefab, dropPosition, Quaternion.identity);

        PickupItem pickup = loot.GetComponent<PickupItem>();
        if (pickup != null)
        {
            pickup.itemData = itemToDrop;
            pickup.quantity = quantity;
        }
    }
}
