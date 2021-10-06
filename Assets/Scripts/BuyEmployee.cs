using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyEmployee : MonoBehaviour, ITouchable
{
    public void OnTouch()
    {
        Debug.Log("touched");
    }
}