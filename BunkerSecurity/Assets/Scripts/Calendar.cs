using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calendar : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dateTxt;

    int currentDay = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetTodaysDate();
    }

    public void SetTodaysDate()
    {
        dateTxt.text = System.DateTime.Today.ToShortDateString();
        currentDay = 0;
    }

    public void UpdateDate(int v)
    {
        System.DateTime ndate = System.DateTime.Today.AddDays(currentDay + v);
        dateTxt.text = ndate.ToShortDateString();
    }
}
