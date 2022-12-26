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
    public static string time;

    private void Awake()
    {
        timer = Stopwatch.StartNew();
        Finish.onFinishGame += StopTimer;
    }

    private void StopTimer()
    {
        timer.Stop();
        TimeSpan time = timer.Elapsed;
        TimerScript.time = $"Time: {time.Minutes}:{time.Seconds}.{time.Milliseconds}";
    }

    private void Update()
    {
        TimeSpan time = timer.Elapsed;
        timerText.text = TimerScript.time = $"Time: {time.Minutes}:{time.Seconds}.{time.Milliseconds}";
    }

}
