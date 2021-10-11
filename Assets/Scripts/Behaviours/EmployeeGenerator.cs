using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeGenerator : MonoBehaviour
{
    [SerializeField] int refreshDelaySeconds;

    [Header("Employee qualities generation from -100 to 200")]
    [SerializeField] int lowest1;
    [SerializeField] int highest1;

    [Header("Employee qualities generation from -100 to 100")]
    [SerializeField] int lowest2;
    [SerializeField] int highest2;

    Employee[] currentEmps = new Employee[2];

    int timer;
    int index;
    int modifier;

    private void OnEnable()
    {
        modifier = GetComponent<TheatreBehaviour>().GetModifier();
        StartCoroutine(DelayInitialRefresh());
    }

    public Employee GenerateEmployee()
    {
        Employee temp = new Employee(Random.Range(lowest1, highest1), Random.Range(lowest1, highest1),
            Random.Range(lowest2, highest2), Random.Range(lowest2, highest2), Random.Range(lowest2, highest2), (EmpType) Random.Range(0, 3), 
            modifier * 100);

        return temp;
    }

    public void RefreshEmployees()
    {
        for (int i = 0; i < currentEmps.Length; i++)
            currentEmps[i] = GenerateEmployee();

        if (App.gridControl.GetTheatreIndex() == index)
            App.empShopScreen.SetEmps(currentEmps);
    }

    IEnumerator ManageRefresh()
    {
        RefreshEmployees();
        timer = refreshDelaySeconds;
        
        while (timer > 0)
        {
            if (App.gridControl.GetTheatreIndex() == index)
                App.empShopScreen.UpdateTimer(timer);
            yield return new WaitForSeconds(1);
            timer -= 1;
        }

        StartCoroutine(ManageRefresh());
    }

    public Employee[] GetCurrentEmps()
    {
        return currentEmps;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    IEnumerator DelayInitialRefresh()
    {
        while (App.empShopScreen == null)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(ManageRefresh());
    }

    public void RemoveEmp(Employee emp)
    {
        for (int i = 0; i < currentEmps.Length; i++)
        {
            if (currentEmps[i] == emp)
            {
                currentEmps[i] = null;
                return;
            }
        }
    }
}