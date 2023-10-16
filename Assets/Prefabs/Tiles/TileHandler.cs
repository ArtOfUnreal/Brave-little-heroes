using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }



    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isTowerPlaced = towerPrefab.CreateTower(towerPrefab, this.transform.position);
            isPlaceable = !isTowerPlaced;
        }
    }
    
}
