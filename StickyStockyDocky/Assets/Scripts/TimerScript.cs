using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;

public class TimerScript : MonoBehaviour
{
    public TMP_Text timerText;
    Stopwatch timer;

    private void Awake()
    {
        timer = Stopwatch.StartNew();
    }

    private void Update()
    {
        TimeSpan time = timer.Elapsed;

        timerText.text = $"Time: {time.Minutes}:{time.Seconds}.{time.Milliseconds}";
    }
}
