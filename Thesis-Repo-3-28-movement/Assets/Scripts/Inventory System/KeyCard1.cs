/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard1 : Item
{
    public string accessLevel = "Level 1";

    private void Awake()
    {
        // Initialize item-specific properties
        itemName = "KeyCard1";
        quantity = 1;
        description = "A keycard that grants access to Level 1 areas.";
        // Assume itemIcon is assigned in the Inspector
    }

    public override void OnPickup()
    {
        base.OnPickup();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerUnit"))
        {
            Debug.Log("KeyCard picked up!");

            // Create an instance of KeyCard1, not a generic Item
            KeyCard1 keyCard = new KeyCard1();  // Create a KeyCard1 instance

            // Add this instance to the inventory
            InventoryManager.Instance.AddItem(keyCard);  // Now we're adding a KeyCard1 to the inventory
        }
    }
}
*/