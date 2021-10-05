using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreBehaviour : MonoBehaviour
{
    Theatre data;

    private void Start()
    {
        data = new Theatre(10);
    }
}