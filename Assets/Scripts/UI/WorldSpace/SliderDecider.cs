using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDecider : MonoBehaviour
{
    [SerializeField] Slider left;
    [SerializeField] Slider right;

    public void SetValue(float value)
    {
        if (value < 0)
        {
            left.value = value;
            right.value = 0;
        }
        else if (value > 0)
        {
            left.value = 0;
            right.value = value;
        }
        else
        {
            left.value = 0;
            right.value = 0;
        }
    }
}