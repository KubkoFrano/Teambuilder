using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeGenerator : MonoBehaviour
{ 
    [Header("Employee qualities generation from -100 to 200")]
    [SerializeField] int lowest1;
    [SerializeField] int highest1;

    [Header("Employee qualities generation from -100 to 100")]
    [SerializeField] int lowest2;
    [SerializeField] int highest2;

    public Employee GenerateEmployee()
    {
        Employee temp = new Employee(Random.Range(lowest1, highest1), Random.Range(lowest1, highest1),
            Random.Range(lowest2, highest2), Random.Range(lowest2, highest2), Random.Range(lowest2, highest2), (EmpType) Random.Range(0, 3));

        return temp;
    }
}