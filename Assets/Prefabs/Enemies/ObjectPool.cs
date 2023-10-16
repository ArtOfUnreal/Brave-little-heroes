using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField][Range(1,50)] int poolSize = 5;
    [SerializeField][Range(0.1f,30f)] float timeBetweenSpawn = 1f;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyToSpawn);
            pool[i].SetActive(false);
            pool[i].transform.parent = transform;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(timeBetweenSpawn);

            //yield return new WaitForSeconds(waitTime);
        }
    }

    void EnableObjectInPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return;
            }
        }
    }


}
