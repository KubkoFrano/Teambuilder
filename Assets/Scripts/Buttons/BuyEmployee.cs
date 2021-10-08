using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyEmployee : MonoBehaviour, ITouchable
{
    TheatreBehaviour theatre;

    private void Awake()
    {
        theatre = GetComponentInParent<TheatreBehaviour>();
    }

    public void OnTouch()
    {
        AddEmployee();
    }

    void AddEmployee()
    {
        theatre.AddEmployee(new Employee(-50, 50, -30, 40, 100, EmpType.reziser));
    }
}