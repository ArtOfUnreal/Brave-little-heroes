using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int killReward = 25;
    [SerializeField] int failPenalty = 25;

    Bank bank;
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void GiveReward()
    {
        if (bank == null) 
        {
            return; 
        }
        else
        {
            bank.Deposit(killReward);
        }
    }

    public void GivePenalty()
    {
        if (bank == null)
        {
            return;
        }
        else
        {
            bank.Withdraw(failPenalty);
        }
    }


}
