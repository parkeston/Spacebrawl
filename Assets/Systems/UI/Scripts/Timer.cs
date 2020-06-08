using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    private TMP_Text text;
    private Coroutine timerCoroutine;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        timerCoroutine = StartCoroutine(StartTimer());
    }

    private void OnDisable()
    {
        if(timerCoroutine!=null)
            StopCoroutine(timerCoroutine);
    }

    private IEnumerator StartTimer()
    {
        var timeSpan = new TimeSpan();
        text.text = timeSpan.ToString(@"mm\:ss");

        while (true)
        {
            yield return new WaitForSeconds(1);
            timeSpan = timeSpan.Add(new TimeSpan(0, 0, 1));
            text.text = timeSpan.ToString(@"mm\:ss");
        }
    }
}
