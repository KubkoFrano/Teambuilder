using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeBehaviour : MonoBehaviour
{
    [SerializeField] EmployeePresets[] presets;

    [SerializeField] SpriteRenderer avatar;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] Slider extroIntro;
    [SerializeField] Slider soloTeam;
    [SerializeField] SliderDecider skill;
    [SerializeField] SliderDecider motivation;
    [SerializeField] SliderDecider reliability;

    Employee data;

    public void Initiate(Employee data)
    {
        this.data = data;
        SetPresets(data.GetEmpType());
        SetSliders();
    }

    void SetSliders()
    {
        extroIntro.value = data.GetExtroIntro();
        soloTeam.value = data.GetSoloTeam();
        skill.SetValue(data.GetSkill());
        motivation.SetValue(data.GetMovivation());
        reliability.SetValue(data.GetReliability());
    }

    void SetPresets(EmpType empType)
    {
        EmployeePresets presets = FindPresets(empType);

        avatar.sprite = presets.GetPicture();
        typeText.text = presets.GetName();
    }

    EmployeePresets FindPresets(EmpType empType)
    {
        foreach (EmployeePresets p in presets)
            if (p.GetEmpType() == empType)
                return p;

        return null;
    }

    public Employee GetEmployee()
    {
        return data;
    }
}