using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Player data;

    private void Start()
    {
        App.playerBehaviour = this;
        data = new Player(0, 2);
    }

    public void AddMoney(int toAdd)
    {
        data.money += toAdd;
    }

    public void AddTheatre(TheatreBehaviour theatre)
    {
        data.theatres.Add(theatre);
        data.theatreCount++;
    }

    public int GetTheatreCount()
    {
        return data.theatreCount;
    }

    public List<TheatreBehaviour> GetTheatres()
    {
        return data.theatres;
    }
}