using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript
{
    public event Action<float> OnTimerValueChangedEvent;
    public event Action OnTimerFinishedEvent;

    public TimerTypeScript type { get; }
    public float remainingSeconds { get; private set; }
    public bool isPaused { get; private set; }

    public TimerScript(TimerTypeScript type)
    {
        this.type = type;
    }

    public TimerScript(TimerTypeScript type, float seconds)
    {
        this.type = type;
        SetTime(seconds);
    }

    public void SetTime(float seconds)
    {
        remainingSeconds = seconds;
        OnTimerValueChangedEvent?.Invoke(remainingSeconds); 
    }

    public void Start()
    {
        if(remainingSeconds == 0)
        {
            Debug.LogError("TIMER: You are trying to start timer with remaining seconds equal 0");
            OnTimerFinishedEvent?.Invoke();
        }

        isPaused = false;
        Subscribe();
        OnTimerValueChangedEvent?.Invoke(remainingSeconds); 
    }

    public void Start(float seconds)
    {
        // OnTimerValueChangedEvent срабатывает здесь дважды
        SetTime(seconds);
        Start();
    }

    public void Pause()
    {
        isPaused = true;
        UnSubscribe();
        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }

    public void UnPause()
    {
        isPaused = false;
        Subscribe();
        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }

    public void Stop()
    {
        UnSubscribe();
        remainingSeconds = 0;

        OnTimerValueChangedEvent?.Invoke(remainingSeconds);
        OnTimerFinishedEvent?.Invoke();
    }

    private void Subscribe()
    {
        switch (type)
        {
            case TimerTypeScript.UpdateTick:
                TimerInvokerScript.instance.OnUpdateTimeTickedEvent += OnUpdateTick;
                break;
            case TimerTypeScript.UpdateTickUnscaled:
                TimerInvokerScript.instance.OnUpdateTimeUnscaledTickedEvent += OnUpdateTick;
                break;
            case TimerTypeScript.OneSecTick:
                TimerInvokerScript.instance.OnOneSecondTickedEvent += OnOneSecondTick;
                break;
            case TimerTypeScript.OneSecTickUnscaled:
                TimerInvokerScript.instance.OnOneSecondUnscaledTickedEvent += OnOneSecondTick;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UnSubscribe()
    {
        switch (type)
        {
            case TimerTypeScript.UpdateTick:
                TimerInvokerScript.instance.OnUpdateTimeTickedEvent -= OnUpdateTick;
                break;
            case TimerTypeScript.UpdateTickUnscaled:
                TimerInvokerScript.instance.OnUpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                break;
            case TimerTypeScript.OneSecTick:
                TimerInvokerScript.instance.OnOneSecondTickedEvent -= OnOneSecondTick;
                break;
            case TimerTypeScript.OneSecTickUnscaled:
                TimerInvokerScript.instance.OnOneSecondUnscaledTickedEvent -= OnOneSecondTick;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnUpdateTick(float deltaTime)
    {
        if (isPaused) return;

        remainingSeconds -= deltaTime;
        CheckFinish();
    }
     
    private void OnOneSecondTick()
    {
        if (isPaused) return;

        remainingSeconds -= 1f;
        CheckFinish();
    }

    private void CheckFinish()
    {
        if (remainingSeconds <= 0) Stop();
        else OnTimerValueChangedEvent?.Invoke(remainingSeconds);
    }
}
