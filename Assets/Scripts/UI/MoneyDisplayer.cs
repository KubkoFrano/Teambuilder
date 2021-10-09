using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplayer : MonoBehaviour
{
    [SerializeField] Color32 plusColor;
    [SerializeField] Color32 minusColor;
    [SerializeField] Color32 zeroColor;

    [Header("Do not touch")]
    [SerializeField] TextMeshProUGUI balanceText;
    [SerializeField] TextMeshProUGUI incomeText;

    private void Awake()
    {
        App.moneyDisplayer = this;
    }

    private void Start()
    {
        RefreshBalance(App.playerBehaviour.GetBalance());
        RefreshIncome(App.playerBehaviour.GetIncome());
        App.playerBehaviour.StartMakingMoney();
    }

    public void RefreshBalance(int balance)
    {
        balanceText.text = balance.ToString() + " $";
    }

    public void RefreshIncome(int income)
    {
        string result = "";

        if (income > 0)
        {
            result += "+ ";
            incomeText.color = plusColor;
        }
        else if (income < 0)
        {
            result += "- ";
            incomeText.color = minusColor;
        }
        else
            incomeText.color = zeroColor;

        result += Mathf.Abs(income).ToString();

        incomeText.text = result;
    }
}