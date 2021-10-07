using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBehaviour : MonoBehaviour
{
    Employee data;

    public void Initiate(Employee data)
    {
        this.data = data;
    }

    public Employee GetEmployee()
    {
        return data;
    }
}