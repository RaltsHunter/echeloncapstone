using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public static GridManager Instance { get; private set; }

    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y);
                grid.Add(cords, new Node(cords));
            }
        }
    }


    public Node GetNodeAtPosition(Vector2Int gridPosition)
    {
        if (grid.ContainsKey(gridPosition))
        {
            return grid[gridPosition];
        }
        return null;  // If the tile doesn't exist, return null
    }

    public GameObject GetTileGameObjectAtPosition(Vector2Int gridPosition)
    {
        // Find all the child tiles under the GridManager object or another parent object
        foreach (Transform tile in transform)
        {
            Labeller labeller = tile.GetComponent<Labeller>();

            if (labeller != null && labeller.cords == gridPosition)
            {
                // Return the GameObject of the tile that matches the grid position
                return tile.gameObject;
            }
        }
        return null;  // Return null if no tile matches the grid position
    }
}
