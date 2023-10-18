using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost = 50;
    [SerializeField] float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(BuildCoroutineWrapper());
    }

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

    IEnumerator BuildCoroutineWrapper()
    {
        yield return StartCoroutine(Build());
        GetComponent<TowerShooting>().StartShoot();
    }

    IEnumerator Build()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

}
