using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]

public class EnemyDamageSystem : MonoBehaviour
{
    [SerializeField] [Min(1f)] float maxHP = 5f;

    [Tooltip("Adds amount to maxHP when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;
    float currentHP;
    Enemy enemy;

    void OnEnable()
    {
        currentHP = maxHP;
    }

    private void Start()
    {
        enemy = this.GetComponent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= float.Epsilon) 
        {
            RunDeathSequence();
        }
    }

    void RunDeathSequence()
    {
        gameObject.SetActive(false);
        enemy.GiveReward();
        maxHP += difficultyRamp;
    }

}
