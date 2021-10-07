using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theatre
{
    public int empCount = 0;
    public int income;
    public bool isUnlocked;
    public List<Employee> employees = new List<Employee>();
    public Transform buyEmpButton;

    public Theatre(TheatreBehaviour root, int initialIncome, bool isUnlocked, Transform buyEmpButton)
    {
        income = initialIncome;
        this.isUnlocked = isUnlocked;
        this.buyEmpButton = buyEmpButton;
        App.playerBehaviour.AddTheatre(root);
    }
}