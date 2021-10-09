using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int money;
    public int theatreCount;
    public int income;
    public List<TheatreBehaviour> theatres = new List<TheatreBehaviour>();

    public Player(int money, int theatreCount, int income)
    {
        this.money = money;
        this.theatreCount = theatreCount;
        this.income = income;
    }
}