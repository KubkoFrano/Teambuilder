using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Player data;

    private void Start()
    {
        App.playerBehaviour = this;
        data = new Player(0);
    }

    public void AddMoney(int toAdd)
    {
        data.money += toAdd;
    }

    public void AddTheatre(Theatre theatre)
    {
        data.theatres.Add(theatre);
    }
}