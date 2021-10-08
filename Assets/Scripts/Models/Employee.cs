using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    Transform realTransform;

    EmpType empType;
    int introExtro;
    int soloTeam;
    int skill;
    int motivation;
    int reliability;

    public Employee(int introExtro, int soloTeam, int skill, int motivation, int reliability, EmpType empType)
    {
        this.introExtro = introExtro;
        this.soloTeam = soloTeam;
        this.skill = skill;
        this.motivation = motivation;
        this.reliability = reliability;
        this.empType = empType;
    }

    public void SetTransform(Transform t)
    {
        realTransform = t;
    }

    public Transform GetTransform()
    {
        return realTransform;
    }
}