/*using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [TextArea]
    [SerializeField] private string itemDescription;


    private InventoryManager inventoryManager;


    void Start()
    {
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
        if (inventoryManager == null)
        {
            //Debug.LogError("InventoryManager not found.");
            return;
        }

    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            //Debug.Log("Item picked up!");
            inventoryManager.AddItem(itemName, quantity, itemDescription);
            Destroy(gameObject);
        }
    }
}
*/