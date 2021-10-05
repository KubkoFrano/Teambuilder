using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int money;
    public List<Theatre> theatres = new List<Theatre>();

    public Player(int money)
    {
        this.money = money;
    }
}