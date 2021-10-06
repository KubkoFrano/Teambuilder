using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreBehaviour : MonoBehaviour
{
    [SerializeField] GameObject employeePrefab;
    [SerializeField] float employeeOffset;

    Theatre data;

    private void Awake()
    {
        data = new Theatre(10);
    }

    private void Start()
    {
        //Debug
        AddEmployee(new Employee(1, 1, 1, 1, 1));
        AddEmployee(new Employee(1, 1, 1, 1, 1));
    }

    public void AddEmployee(Employee emp)
    {
        data.AddEmployee(emp);
        EmployeeBehaviour tempB = Instantiate(employeePrefab).GetComponent<EmployeeBehaviour>();
        tempB.transform.parent = this.transform;
        tempB.transform.localPosition = new Vector3((data.GetEmpCount() + 1) * employeeOffset, 0, 0);
        tempB.Initiate(emp);
        data.AddEmpCount();
        
        RecalculateIncome();
    }

    public int GetEmpCount()
    {
        return data.GetEmpCount();
    }

    public void RecalculateIncome()
    {
        //Recalculates income based on list of employees in data
    }
}