using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationUnitController : MonoBehaviour
{

    public GameObject selectedUnit;
    bool unitSelected = false;
    GridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
        selectedUnit = GameObject.Find("Hacker");
        unitSelected = true;
        gridManager = FindObjectOfType<GridManager>();
        
    }



    
    void Update()
    {
        gridManager = FindObjectOfType<GridManager>();
        selectedUnit = GameObject.Find("Hacker");
        HandleKeyboardInput();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.Log($"Hit: {hit.transform.name}, Tag: {hit.transform.tag}");

            if (hit.transform.CompareTag("PlayerUnit"))
            {
                //ClearHighlightedTiles(); 

                selectedUnit = hit.transform.gameObject;
                unitSelected = true;

               
                //cameraController.GetComponent<CameraScript>().SnapToUnit(selectedUnit);  

                //selectedUnitCurrentMovementPoints = selectedUnit.GetComponent<Unit>().currentMovementPoints;
                //Debug.Log("Unit Selected");

                //HighlightReachableTiles(); // Highlight reachable tiles
                return;
            }
        }
    }



    void HandleKeyboardInput()
    {
        
        if (selectedUnit != null && unitSelected)
        {
            
            bool moved = false;
            Vector2Int moveDirection = Vector2Int.zero;

            
            float unitRotation = selectedUnit.transform.eulerAngles.y;
            unitRotation = Mathf.Round(unitRotation); 

            
            if (unitRotation == 0) 
            {
                if (Input.GetKeyDown(KeyCode.W)) { moveDirection = new Vector2Int(0, 1); moved = true; Debug.Log("W Pressed"); }
                else if (Input.GetKeyDown(KeyCode.S)) { moveDirection = new Vector2Int(0, -1); moved = true; Debug.Log("S Pressed"); }
                else if (Input.GetKeyDown(KeyCode.A)) { moveDirection = new Vector2Int(-1, 0); moved = true; Debug.Log("A Pressed"); }
                else if (Input.GetKeyDown(KeyCode.D)) { moveDirection = new Vector2Int(1, 0); moved = true; Debug.Log("D Pressed"); }
            }
            else if (unitRotation == 270 || unitRotation == -90) 
            {
                if (Input.GetKeyDown(KeyCode.W)) { moveDirection = new Vector2Int(-1, 0); moved = true; Debug.Log("W Pressed"); }
                else if (Input.GetKeyDown(KeyCode.S)) { moveDirection = new Vector2Int(1, 0); moved = true; Debug.Log("S Pressed"); }
                else if (Input.GetKeyDown(KeyCode.A)) { moveDirection = new Vector2Int(0, -1); moved = true; Debug.Log("A Pressed"); }
                else if (Input.GetKeyDown(KeyCode.D)) { moveDirection = new Vector2Int(0, 1); moved = true; Debug.Log("D Pressed"); }
            }
            else if (unitRotation == 180 || unitRotation == -180) // Facing South
            {
                if (Input.GetKeyDown(KeyCode.W)) { moveDirection = new Vector2Int(0, -1); moved = true; Debug.Log("W Pressed"); }
                else if (Input.GetKeyDown(KeyCode.S)) { moveDirection = new Vector2Int(0, 1); moved = true; Debug.Log("S Pressed"); }
                else if (Input.GetKeyDown(KeyCode.A)) { moveDirection = new Vector2Int(1, 0); moved = true; Debug.Log("A Pressed"); }
                else if (Input.GetKeyDown(KeyCode.D)) { moveDirection = new Vector2Int(-1, 0); moved = true; Debug.Log("D Pressed"); }
            }
            else if (unitRotation == 90) // Facing East
            {
                if (Input.GetKeyDown(KeyCode.W)) { moveDirection = new Vector2Int(1, 0); moved = true; Debug.Log("W Pressed"); }
                else if (Input.GetKeyDown(KeyCode.S)) { moveDirection = new Vector2Int(-1, 0); moved = true; Debug.Log("S Pressed"); }
                else if (Input.GetKeyDown(KeyCode.A)) { moveDirection = new Vector2Int(0, 1); moved = true; Debug.Log("A Pressed"); }
                else if (Input.GetKeyDown(KeyCode.D)) { moveDirection = new Vector2Int(0, -1); moved = true; Debug.Log("D Pressed"); }
            }

            if (moved)
            {
                Vector2Int targetGridPosition = new Vector2Int(
                    Mathf.RoundToInt(selectedUnit.transform.position.x / gridManager.UnityGridSize),
                    Mathf.RoundToInt(selectedUnit.transform.position.z / gridManager.UnityGridSize)
                ) + moveDirection;

                GameObject targetTile = gridManager.GetTileGameObjectAtPosition(targetGridPosition);
                if (targetTile != null && !targetTile.CompareTag("Unwalkable"))
                {
                    selectedUnit.transform.position = new Vector3(
                        targetGridPosition.x * gridManager.UnityGridSize,
                        selectedUnit.transform.position.y,
                        targetGridPosition.y * gridManager.UnityGridSize
                    );
                    Debug.Log($"Unit moved to: {targetGridPosition}, Remaining movement points: {selectedUnit.GetComponent<Unit>().currentMovementPoints}");
                }
            }
        }
    }


}
