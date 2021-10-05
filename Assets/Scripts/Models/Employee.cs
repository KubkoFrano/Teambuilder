using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
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
}