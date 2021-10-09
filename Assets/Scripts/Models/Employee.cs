using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    Transform realTransform;

    EmpType empType;
    int extroIntro;
    int soloTeam;
    int skill;
    int motivation;
    int reliability;

    public Employee(int extroIntro, int soloTeam, int skill, int motivation, int reliability, EmpType empType)
    {
        this.extroIntro = extroIntro;
        this.soloTeam = soloTeam;
        this.skill = skill;
        this.motivation = motivation;
        this.reliability = reliability;
        this.empType = empType;
    }

    public int GetIncome()
    {
        return skill + motivation + reliability;
    }

    public void SetTransform(Transform t)
    {
        realTransform = t;
    }

    public Transform GetTransform()
    {
        return realTransform;
    }

    public EmpType GetEmpType()
    {
        return empType;
    }

    public int GetExtroIntro()
    {
        return extroIntro;
    }

    public int GetSoloTeam()
    {
        return soloTeam;
    }

    public int GetSkill()
    {
        return skill;
    }

    public int GetMovivation()
    {
        return motivation;
    }

    public int GetReliability()
    {
        return reliability;
    }
}