using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreBehaviour : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Color32 unlockedColor;
    SpriteRenderer mRenderer;

    [Header("Do not touch")]
    [SerializeField] GameObject employeePrefab;
    [SerializeField] float employeeOffset;
    [SerializeField] bool initUnlocked;
    [SerializeField] Transform buyEmpButton;
    [SerializeField] GameObject buyTheatreButton;

    Theatre data;

    private void Awake()
    {
        data = new Theatre(this, 10, initUnlocked, buyEmpButton);
        mRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (initUnlocked)
            Unlock();
    }

    public void AddEmployee(Employee emp)
    {
        data.employees.Add(emp);
        EmployeeBehaviour tempB = Instantiate(employeePrefab).GetComponent<EmployeeBehaviour>();
        emp.SetTransform(tempB.transform);
        tempB.transform.parent = this.transform;
        tempB.transform.localPosition = new Vector3((data.empCount + 1) * employeeOffset, 0, 0);
        tempB.Initiate(emp);
        data.empCount++;
        App.gridControl.ResfreshLength(transform, data.empCount);
        data.buyEmpButton.gameObject.SetActive(true);
        App.gridControl.PositionEBB(transform, data.buyEmpButton, employeeOffset);
        RecalculateIncome();
    }

    public void RemoveEmployee(EmployeeBehaviour emp)
    {
        data.employees.Remove(emp.GetEmployee());
        data.empCount--;
        Destroy(emp.gameObject);
        App.gridControl.ResfreshLength(transform, data.empCount);
        App.gridControl.RefreshEmpPositions(data.employees, employeeOffset);
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

    public void Unlock()
    {
        data.isUnlocked = true;
        buyTheatreButton.SetActive(false);
        mRenderer.color = unlockedColor;
        data.buyEmpButton.gameObject.SetActive(true);
        App.gridControl.PositionEBB(transform, data.buyEmpButton, employeeOffset);
    }

    public bool IsUnlocked()
    {
        return data.isUnlocked;
    }
}