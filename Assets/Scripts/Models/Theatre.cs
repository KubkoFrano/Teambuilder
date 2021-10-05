using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theatre
{
    int income;

    public Theatre(int initialIncome)
    {
        income = initialIncome;
        App.playerBehaviour.AddTheatre(this);
    }

    public void SetIncome(int income)
    {
        this.income = income;
    }

    public int GetIncome()
    {
        return income;
    }
}