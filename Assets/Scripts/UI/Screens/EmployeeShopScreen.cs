using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmployeeShopScreen : ScreenBase
{
    [SerializeField] EmployeeCard[] cards;
    [SerializeField] ShopTimer timer;
    [SerializeField] TextMeshProUGUI theatreText;

    EmployeeGenerator generator;
    TheatreBehaviour theatre;

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
        theatreText.text = "Theatre " + (App.gridControl.GetTheatreIndex() + 1);
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

    public void SetTheatreBehaviour(TheatreBehaviour theatre)
    {
        this.theatre = theatre;
    }

    public void AddEmployee(Employee emp)
    {
        theatre.AddEmployee(emp);
        generator.RemoveEmp(emp);
    }
}