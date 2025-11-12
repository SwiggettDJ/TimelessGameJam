using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUpdate : MonoBehaviour
{
    private float time;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        time = TimeManager.instance.currentLoopTime;
        text.SetText(time.ToString("0.00"));
    }
    //timeText.text = currentLoopTime.ToString("0.00");
}
