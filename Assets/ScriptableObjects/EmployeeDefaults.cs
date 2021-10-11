using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmployeeDefaults", menuName = "ScriptableObjects/EmployeeDefaults", order = 1)]
public class EmployeeDefaults : ScriptableObject
{
    [SerializeField] EmployeePresets[] presets;

    public EmployeePresets FindPresets(EmpType empType)
    {
        foreach (EmployeePresets p in presets)
            if (p.GetEmpType() == empType)
                return p;

        return null;
    }
}