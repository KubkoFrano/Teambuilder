using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTheatre : MonoBehaviour, ITouchable
{
    [SerializeField] int tapCash;
    [SerializeField] int textsNumber;
    [SerializeField] float offsetH;
    [SerializeField] float offsetV;

    [Header("Do not touch")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject tapPrefab;

    TheatreBehaviour theatre;
    RectTransform rect;

    TapCash[] taps;

    private void Awake()
    {
        theatre = GetComponent<TheatreBehaviour>();
        rect = canvas.GetComponent<RectTransform>();

    }

    private void Start()
    {
        taps = new TapCash[textsNumber];

        for (int i = 0; i < taps.Length; i++)
        {
            GameObject temp = Instantiate(tapPrefab, canvas.transform);
            taps[i] = temp.GetComponent<TapCash>();
        }
    }

    public void OnTouch()
    {
        if (theatre.IsUnlocked())
        {
            App.playerBehaviour.AddMoney(theatre.GetModifier() * tapCash);

            FindTap(new Vector3(Random.Range(-rect.rect.width+offsetH, rect.rect.width-offsetH), 
                Random.Range(-rect.rect.height+offsetV, rect.rect.height-offsetV), 0));
        }
    }

    void FindTap(Vector3 position)
    {
        foreach (TapCash tap in taps)
            if (tap.Play(position, theatre.GetModifier() * tapCash))
                return;
    }
}