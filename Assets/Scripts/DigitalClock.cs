using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    string hour, minute;
    [Range(1, 12)]
    public int startHour = 9;
    int hourOffset;
    DateTime timeNow;
    // Start is called before the first frame update
    void Start()
    {
        hourOffset = DateTime.Now.Minute - (startHour - 1);
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeNow = DateTime.Now;
        hour = Remainder12(timeNow.Minute - hourOffset).ToString().PadLeft(2, '0');
        minute = timeNow.Second.ToString().PadLeft(2, '0');
        textMeshPro.text = hour + ":" + minute;
    }
    int Remainder12(int i)
    {
        return (i % 12 + 1);
    }
}
