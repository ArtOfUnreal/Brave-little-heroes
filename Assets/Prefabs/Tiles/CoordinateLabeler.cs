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
    [SerializeField] Color occupiedColor = Color.grey;

    static readonly float worldGridStep = 10f;

    TextMeshPro label;
    TileHandler tileHandler;
    Vector2Int coordinates = new Vector2Int();
    // Update is called once per frame

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        tileHandler = gameObject.GetComponentInParent<TileHandler>();
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
        if (tileHandler.IsPlaceable)
        {
            label.color = emptyColor;
        }
        else
        {
            label.color = occupiedColor;
        }
    }

    private void UpdateTileName()
    {
        transform.parent.name = "Tile " + coordinates.ToString();
    }
}
