using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 100;
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }
    GoldUI goldUI;

    private void Start()
    {
        goldUI = FindObjectOfType<GoldUI>();
    }

    void Awake()
    {
        currentBalance = startBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        goldUI.UpdateGoldText();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        goldUI.UpdateGoldText();

        if (currentBalance < 0)
        {
            //Lose the game
            LoseSequence();
        }
    }

    void LoseSequence()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }


}
