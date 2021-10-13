using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TapCash : MonoBehaviour
{
    [SerializeField] float fadeoutSpeed;
    [SerializeField] Color32 baseColor;

    [Header("Do not touch")]
    [SerializeField] TextMeshProUGUI text;

    RectTransform rect;

    bool isDisplayed = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public bool Play(Vector3 position, int cashValue)
    {
        if (isDisplayed)
            return false;
        else
        {
            text.text = "+" + cashValue + " $";
            isDisplayed = true;
            rect.localPosition = position;
            text.color = baseColor;
            text.enabled = true;

            StartCoroutine(FadeOut());
            return true;
        }
    }

    IEnumerator FadeOut()
    {
        float f = baseColor.a;

        for (; f > 0; f -= fadeoutSpeed)
        {
            if (f < 0)
                f = 0;

            text.color = new Color32(baseColor.r, baseColor.g, baseColor.b, (byte) f);
            yield return new WaitForFixedUpdate();
        }

        text.enabled = false;
        isDisplayed = false;
    }
}