using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyTheatre : MonoBehaviour, ITouchable
{
    [SerializeField] int price;

    [Header("Do not touch")]
    [SerializeField] TextMeshProUGUI text;

    TheatreBehaviour theatre;

    private void Start()
    {
        theatre = GetComponentInParent<TheatreBehaviour>();
        text.text = price + " $";
    }

    public void OnTouch()
    {
        if (App.playerBehaviour.SpendMoney(price))
            theatre.Unlock();
    }
}