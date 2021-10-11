using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTimer : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetTime(int timeInSeconds)
    {
        text.text = FormatTime(timeInSeconds) + " to refresh";
    }

    string FormatTime(int time)
    {
        string result = "";

        result += (time / 60 < 10) ? ("0" + time / 60).ToString() : (time / 60).ToString();
        result += ":";
        result += (time % 60 < 10) ? ("0" + time % 60).ToString() : (time % 60).ToString();

        return result;
    }
}