using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatText : MonoBehaviour
{
    [SerializeField] Color32 positiveColor;
    [SerializeField] Color32 negativeColor;
    [SerializeField] Color32 neutralColor;

    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(int value)
    {
        string result;

        if (value > 0)
        {
            result = "+ ";
            text.color = positiveColor;
        }
        else if (value < 0)
        {
            result = "- ";
            text.color = negativeColor;
        }
        else
        {
            result = "";
            text.color = neutralColor;
        }

        result += Mathf.Abs(value) + " $ /sec";
        text.text = result;
    }
}
