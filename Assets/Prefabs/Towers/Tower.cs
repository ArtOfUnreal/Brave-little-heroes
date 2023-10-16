using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 50;
    
    public bool CreateTower(Tower tower, Vector3 worldPosition)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= towerCost)
        {
            bank.Withdraw(towerCost);
            Instantiate(tower.gameObject, worldPosition, Quaternion.identity);
            return true;
        }
        return false;
    }

}
