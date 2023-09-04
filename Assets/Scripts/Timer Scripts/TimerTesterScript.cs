using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTesterScript : MonoBehaviour
{
    [SerializeField] private TimerTypeScript _type;
    [SerializeField] private float _timerSeconds;

    private TimerScript _timer;

    private void Awake()
    {
        _timer = new TimerScript(_type, _timerSeconds);
        _timer.OnTimerValueChangedEvent += OnTimerValueChanged;
        _timer.OnTimerFinishedEvent += OnTimerFinished;
    }

    private void OnDestroy()
    {
        _timer.OnTimerValueChangedEvent -= OnTimerValueChanged;
        _timer.OnTimerFinishedEvent -= OnTimerFinished;
    }

    private void OnTimerValueChanged(float remainingSeconds)
    {
        Debug.Log($"Timer ticked. Remaining seconds: {remainingSeconds}");
    }

    private void OnTimerFinished()
    {
        Debug.Log($"TIMER FINISHED");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) StartTimerClicked();
        if (Input.GetKeyDown(KeyCode.Space)) PauseTimerClicked();
        if (Input.GetKeyDown(KeyCode.S)) StopTimerClicked();
    }

    private void StartTimerClicked()
    {
        _timer.Start();
    }

    private void PauseTimerClicked()
    {
        if (_timer.isPaused) _timer.UnPause();
        else _timer.Pause();    
    }

    private void StopTimerClicked()
    {
        _timer.Stop();
    }
}
