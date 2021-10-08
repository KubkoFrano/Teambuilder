using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialLoader : MonoBehaviour
{
    List<TheatreBehaviour> theatres;

    private void Start()
    {
        theatres = App.playerBehaviour.GetTheatres();
        theatres[0].AddEmployee(new Employee(10, 10, 10, 10, 10, EmpType.herec));
    }
}