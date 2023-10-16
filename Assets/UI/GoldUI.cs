using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] Color normalTextColor = Color.green;
    [SerializeField] Color negativeTextColor = Color.red;
    TMP_Text goldText;
    Bank bank;

    void Start()
    {
        goldText = this.GetComponent<TMP_Text>();
        bank = FindObjectOfType<Bank>();
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        if ( bank != null )
        {
            
            goldText.text = "Gold : " + bank.CurrentBalance;

            if (bank.CurrentBalance >= 0 )
            {
                goldText.color = normalTextColor;
            }
            else
            {
                goldText.color = negativeTextColor;
            }
        }
    }

}
