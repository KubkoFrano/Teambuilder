using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeShopScreen : ScreenBase
{
    [SerializeField] EmployeeCard[] cards;
    [SerializeField] ShopTimer timer;

    EmployeeGenerator generator;

    private void Awake()
    {
        App.empShopScreen = this;
    }

    public override void Show()
    {
        base.Show();
        App.gridControl.SetSwipe(false);
        generator = App.gridControl.GetCurrentGenerator();
        SetEmps(generator.GetCurrentEmps());
    }

    public override void Hide()
    {
        App.gridControl.SetSwipe(true);
        base.Hide();
    }

    public void SetEmps(Employee[] emps)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].CreateCard(emps[i]);
        }
    }

    public void UpdateTimer(int time)
    {
        timer.SetTime(time);
    }
}