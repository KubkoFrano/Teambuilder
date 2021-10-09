using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Player data;

    private void Start()
    {
        App.playerBehaviour = this;
        data = new Player(0, 2, 0);
        CalculateIncome();
    }

    public void StartMakingMoney()
    {
        StartCoroutine(MakeMoney());
    }

    public void StopMakingMoney()
    {
        StopCoroutine(MakeMoney());
    }

    IEnumerator MakeMoney()
    {
        while (true)
        {
            data.money += data.income;
            yield return new WaitForSeconds(1);
        }
    }

    public void RefreshMoney()
    {
        App.moneyDisplayer.RefreshBalance(data.money);
    }

    public void CalculateIncome()
    {
        int income = 0;

        foreach (TheatreBehaviour t in data.theatres)
            income += t.GetIncome();

        data.income = income;
    }

    public void RefreshIncome()
    {
        App.moneyDisplayer.RefreshIncome(data.income);
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

    public int GetBalance()
    {
        return data.money;
    }

    public int GetIncome()
    {
        return data.income;
    }
}