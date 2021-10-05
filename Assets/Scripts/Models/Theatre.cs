using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theatre
{
    int empCount = 0;
    int income;
    List<Employee> employees = new List<Employee>();

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

    public void AddEmployee(Employee emp)
    {
        employees.Add(emp);
    }

    public void AddEmpCount()
    {
        empCount++;
    }

    public void SubtractEmpCount()
    {
        empCount--;
    }

    public int GetEmpCount()
    {
        return empCount;
    }
}