using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySystem : MonoBehaviour
{
    public TurnManager turnManager; // Reference to the TurnManager script

    // Trigger collision with player unit
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");

        // Check if the colliding object is a player unit with the name "Hacker"
        if (other.CompareTag("PlayerUnit") && other.gameObject.name == "Hacker")
        {
            // Add 10 turns to the round counter
            turnManager.GetComponent<TurnManager>().roundCounter += 10;
            Debug.Log("Hacked the security system. You have more time!");

            // Optional: Disable the trigger after the collision so it can't be triggered again
            gameObject.SetActive(false);
        }
    }
}
