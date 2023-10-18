using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color emptyColor = Color.white;
    [SerializeField] Color notWalkableColor = Color.grey;
    [SerializeField] Color isExploredColor = Color.yellow;
    [SerializeField] Color isPathColor = new Color(1f, 0.5f, 0f);


    float worldGridStep = 10f;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        worldGridStep = gridManager.WorldGridStep;
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }

        SetCoordinatesLabelColor();
        ToggleLabelVisibility();
        
    }

    
    
    void ToggleLabelVisibility()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            label.enabled = !label.enabled;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / worldGridStep);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / worldGridStep);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void SetCoordinatesLabelColor()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return; }

        if (!node.isWalkable) 
        {
            label.color = notWalkableColor; 
        }
        else if (node.isPath) 
        { 
            label.color = isPathColor; 
        }
        else if (node.isExplored)
        { 
            label.color = isExploredColor; 
        }
        else 
        { 
            label.color = emptyColor;
        }
        
    }

    private void UpdateTileName()
    {
        transform.parent.name = "Tile " + coordinates.ToString();
    }
}
