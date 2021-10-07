using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int money;
    public int theatreCount;
    public List<TheatreBehaviour> theatres = new List<TheatreBehaviour>();

    public Player(int money, int theatreCount)
    {
        this.money = money;
        this.theatreCount = theatreCount;
    }
}