using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject selectedUnit; // Reference to the selected unit's transform
    public GameObject unitController; // Reference to the UnitController script
    public float cameraHeight = 10f; // Height of the camera above the unit
    public float snapSpeed = 5f; // Speed at which the camera snaps to the selected unit
    public float moveSpeed = 5f; // Speed at which the camera moves with arrow keys

    private Vector3 targetPosition;

    void Update()
    {

        MoveCameraWithArrowKeys();
        selectedUnit = unitController.GetComponent<UnitController>().selectedUnit;
    }

    public void SnapToUnit(GameObject newSelectedUnit)
    {
        if (newSelectedUnit != null)
        {
            // Define the target position above the selected unit
            targetPosition = new Vector3(newSelectedUnit.transform.position.x, newSelectedUnit.transform.position.y + cameraHeight, newSelectedUnit.transform.position.z);

            // Instantly snap the camera to the target position
            transform.position = targetPosition;
        }
    }



    private void MoveCameraWithArrowKeys()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += new Vector3(0, 0, 1); // Move forward (along Z axis)
            
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += new Vector3(0, 0, -1); // Move backward (along Z axis)
            
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += new Vector3(-1, 0, 0); // Move left (along X axis)
            
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += new Vector3(1, 0, 0); // Move right (along X axis)
            
        }

        // Adjust the position of the camera based on input and moveSpeed
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }


}
