using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreBehaviour : MonoBehaviour
{
    [SerializeField] GameObject employeePrefab;
    [SerializeField] float employeeOffset;
    [SerializeField] bool initUnlocked;
    [SerializeField] Transform buyEmpButton;

    Theatre data;

    private void Awake()
    {
        data = new Theatre(this, 10, initUnlocked, buyEmpButton);
    }

    public void AddEmployee(Employee emp)
    {
        data.employees.Add(emp);
        EmployeeBehaviour tempB = Instantiate(employeePrefab).GetComponent<EmployeeBehaviour>();
        tempB.transform.parent = this.transform;
        tempB.transform.localPosition = new Vector3((data.empCount + 1) * employeeOffset, 0, 0);
        tempB.Initiate(emp);
        data.empCount++;
        App.gridControl.ResfreshLength(transform, data.empCount);
        data.buyEmpButton.gameObject.SetActive(true);
        App.gridControl.PositionEBB(transform, data.buyEmpButton, employeeOffset);
        RecalculateIncome();
    }

    public int GetEmpCount()
    {
        return data.empCount;
    }

    public void RecalculateIncome()
    {
        //Recalculates income based on list of employees in data
    }

    public void SetIncome(int income)
    {
        data.income = income;
    }

    public int GetIncome()
    {
        return data.income;
    }

    public bool IsUnlocked()
    {
        return data.isUnlocked;
    }
}