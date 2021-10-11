using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeCard : MonoBehaviour
{
    [Header("Do not touch")]
    [SerializeField] EmployeeDefaults defaults;
    [SerializeField] Image avatar;
    [SerializeField] TextMeshProUGUI empTypeText;
    [SerializeField] SliderDecider skill;
    [SerializeField] SliderDecider motivation;
    [SerializeField] SliderDecider reliability;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject blank;

    Employee currentEmp;

    public void CreateCard(Employee emp)
    {
        currentEmp = emp;

        if (emp == null)
        {
            blank.SetActive(true);
            return;
        }
        else
            blank.SetActive(false);

        EmployeePresets presets = defaults.FindPresets(emp.GetEmpType());

        avatar.sprite = presets.GetPicture();
        empTypeText.text = presets.GetName();

        skill.SetValue(emp.GetSkill());
        motivation.SetValue(emp.GetMovivation());
        reliability.SetValue(emp.GetReliability());
        priceText.text = emp.GetPrice() + " $";
    }

    public void BuyEmployee()
    {
        if (App.playerBehaviour.SpendMoney(currentEmp.GetPrice()))
        {
            App.empShopScreen.AddEmployee(currentEmp);
            CreateCard(null);
        }
    }
}