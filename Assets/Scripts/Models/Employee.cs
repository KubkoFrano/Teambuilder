using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    Transform realTransform;

    int introExtro;
    int soloTeam;
    int skill;
    int motivation;
    int reliability;

    public Employee(int introExtro, int soloTeam, int skill, int motivation, int reliability)
    {
        this.introExtro = introExtro;
        this.soloTeam = soloTeam;
        this.skill = skill;
        this.motivation = motivation;
        this.reliability = reliability;
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