using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target = null;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectileSpawner;
    [SerializeField] int projectilesPoolSize = 5;
    [SerializeField] [Min(0.1f)] float projectileDamage = 1f;
    [SerializeField] [Range(0.05f,5f)] float attackSpeed = 1f;
    [SerializeField] float shootRange = 20f;

    GameObject[] projectilesPool;

    void Awake()
    {
        PopulatePool();
        StartCoroutine(TryShoot());
    }

    void Update()
    {
        if ((target == null) || (target.gameObject.activeInHierarchy == false) || (Vector3.Distance(this.transform.position, target.transform.position) > shootRange))
        {
            FindClosestTarget();
        }
        else
        {
            AimWeapon(); 
        }
    }

    void PopulatePool()
    {
        projectilesPool = new GameObject[projectilesPoolSize];
        for (int i = 0; i < projectilesPool.Length; i++)
        {
            projectilesPool[i] = Instantiate(projectile);
            projectilesPool[i].SetActive(false);
            projectilesPool[i].transform.parent = transform;
        }
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float minDistance = shootRange; //Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (targetDistance < minDistance) 
            {
                closestTarget = enemy.transform;
                minDistance = targetDistance;
            }
        }
        target  = closestTarget; 
    }

    void AimWeapon()
    {
        weapon.LookAt(target);
    }

    IEnumerator TryShoot()
    {
        while (true)
        {
            if ((target != null) && (target.gameObject.activeInHierarchy))
            {
                EnableObjectInPool();
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    void EnableObjectInPool()
    {
        foreach (GameObject obj in projectilesPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.GetComponent<ProjectileHandler>().ActivateProjectile(projectileSpawner.transform.position, target.gameObject, projectileDamage);
                return;
            }
        }
    }

}
