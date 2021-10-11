using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeShopScreen : ScreenBase
{
    public override void Show()
    {
        base.Show();
        App.gridControl.SetSwipe(false);
    }

    public override void Hide()
    {
        App.gridControl.SetSwipe(true);
        base.Hide();
    }
}