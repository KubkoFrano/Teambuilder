using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTheatre : MonoBehaviour, ITouchable
{
    TheatreBehaviour theatre;

    private void Start()
    {
        theatre = GetComponentInParent<TheatreBehaviour>();
    }

    public void OnTouch()
    {
        theatre.Unlock();
    }
}