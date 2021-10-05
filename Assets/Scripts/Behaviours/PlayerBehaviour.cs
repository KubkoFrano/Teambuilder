using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Player player;

    private void Start()
    {
        App.playerBehaviour = this;
        player = new Player(0);
    }

    public void AddMoney(int toAdd)
    {
        player.money += toAdd;
    }
}