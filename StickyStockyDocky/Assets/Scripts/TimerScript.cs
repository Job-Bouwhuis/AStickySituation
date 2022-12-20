using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    public TMP_Text timerText;
    public TimeSpan ts;

    private void Update()
    {
        ts = TimeSpan.FromSeconds(Time.timeAsDouble);
        timerText.text = "Time : " + ts.Minutes + "." + ts.Seconds + "." + ts.Milliseconds;
    }
}
