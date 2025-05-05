using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROTOTYPEfleescript : MonoBehaviour
{
    [SerializeField] public GameObject fleeButton;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");

        // Check if the colliding object is a player unit with the name "Hacker"
        if (other.CompareTag("PlayerUnit") && other.gameObject.name == "Hacker")
        {
           fleeButton.SetActive(true);
        }
    }
}
