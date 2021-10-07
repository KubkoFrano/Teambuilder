using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEmployee : MonoBehaviour, ITouchable
{
    public void OnTouch()
    {
        DeleteEmployee();
    }

    void DeleteEmployee()
    {
        GetComponentInParent<TheatreBehaviour>().RemoveEmployee(GetComponentInParent<EmployeeBehaviour>());
    }
}