using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreBehaviour : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] Color32 unlockedColor;
    SpriteRenderer mRenderer;
    
    [Tooltip("Used for calculating income")]
    [SerializeField] int modifier;

    [Header("Do not touch")]
    [SerializeField] GameObject employeePrefab;
    [SerializeField] float employeeOffset;
    [SerializeField] bool initUnlocked;
    [SerializeField] Transform buyEmpButton;
    [SerializeField] GameObject buyTheatreButton;
    [SerializeField] GameObject statsPanel;
    [SerializeField] StatText skillText;
    [SerializeField] StatText motivationText;
    [SerializeField] StatText reliabilityText;
    [SerializeField] StatText totalText;

    Theatre data;

    private void OnEnable()
    {
        data = new Theatre(this, 0, initUnlocked, buyEmpButton);
        mRenderer = GetComponent<SpriteRenderer>();

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
        int result = 0;
        int skill = 0;
        int motivation = 0;
        int reliability = 0;

        foreach (Employee e in data.employees)
        {
            result += e.GetIncome();
            skill += e.GetSkill();
            motivation += e.GetMovivation();
            reliability += e.GetReliability();
        }

        result *= modifier;
        data.income = result;

        RefreshTheatreStats(skill * modifier, motivation * modifier, reliability * modifier, result);
        App.playerBehaviour.CalculateIncome();
        App.playerBehaviour.RefreshIncome();
    }

    void RefreshTheatreStats(int skill, int motivation, int reliability, int total)
    {
        skillText.SetText(skill);
        motivationText.SetText(motivation);
        reliabilityText.SetText(reliability);
        totalText.SetText(total);
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
        GetComponent<EmployeeGenerator>().enabled = true; ;
        buyTheatreButton.SetActive(false);
        mRenderer.color = unlockedColor;
        data.buyEmpButton.gameObject.SetActive(true);
        App.gridControl.PositionEBB(transform, data.buyEmpButton, employeeOffset);
        statsPanel.SetActive(true);
        RecalculateIncome();
    }

    public bool IsUnlocked()
    {
        return data.isUnlocked;
    }

    public int GetModifier()
    {
        return modifier;
    }

    //Debug
    public void AddRandom()
    {
        AddEmployee(GetComponent<EmployeeGenerator>().GenerateEmployee());
    }
}