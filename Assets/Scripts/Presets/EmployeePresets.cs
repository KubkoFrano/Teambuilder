using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EmployeePresets
{
    [SerializeField] EmpType empType;
    [SerializeField] string name;
    [SerializeField] Sprite picture;

    public EmpType GetEmpType()
    {
        return empType;
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetPicture()
    {
        return picture;
    }
}