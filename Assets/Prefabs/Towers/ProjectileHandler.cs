using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float projectileSpeed = 1000;
    float projectileDamage = 1f;

    public void ActivateProjectile(Vector3 shooterPosition, GameObject weaponTarget, float damage)
    {
        this.transform.position = shooterPosition;
        target = weaponTarget;
        projectileDamage = damage;
        this.transform.LookAt(target.transform);
        gameObject.SetActive(true);
        GetComponent<Rigidbody>().AddForce(this.transform.forward * projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyDamageSystem enemyDS = other.gameObject.GetComponent<EnemyDamageSystem>();
            enemyDS.TakeDamage(projectileDamage);
            gameObject.SetActive(false);
        }
        if ((other.gameObject.tag == "Enviroment") || (other.gameObject.tag == "Path"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
