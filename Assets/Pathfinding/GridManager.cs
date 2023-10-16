using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2 gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++) 
        {
            for(int y = 0; y < gridSize.y; y++) 
            { 
                Vector2Int nodeCoordinates = new Vector2Int(x, y);
                grid.Add(nodeCoordinates, new Node(nodeCoordinates, true));
                Debug.Log(grid[nodeCoordinates].coordinates + "  =  " + grid[nodeCoordinates].isWalkable);
            }
        }
    }
}
