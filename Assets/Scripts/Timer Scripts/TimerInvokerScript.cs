using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerInvokerScript : MonoBehaviour
{
    public event Action<float> OnUpdateTimeTickedEvent;
    public event Action<float> OnUpdateTimeUnscaledTickedEvent;
    public event Action OnOneSecondTickedEvent;
    public event Action OnOneSecondUnscaledTickedEvent;

    public static TimerInvokerScript instance 
    {
        get
        {   
            if(_instance == null)
            {
                var go = new GameObject("[TIME_INVOKER]");
                _instance = go.AddComponent<TimerInvokerScript>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private static TimerInvokerScript _instance;

    private float _oneSecTimer;
    private float _oneSecUnscaledTimer;

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        OnUpdateTimeTickedEvent?.Invoke(deltaTime);

        _oneSecTimer += deltaTime;
        if(_oneSecTimer >= 1f)
        {
            _oneSecTimer -= 1f;
            OnOneSecondTickedEvent?.Invoke();
        }

        float unscaledDeltaTime = Time.unscaledDeltaTime;
        OnUpdateTimeUnscaledTickedEvent?.Invoke(unscaledDeltaTime);

        _oneSecUnscaledTimer += unscaledDeltaTime;
        if(_oneSecUnscaledTimer >= 1f)
        {
            _oneSecUnscaledTimer -= 1f;
            OnOneSecondUnscaledTickedEvent?.Invoke();
        }
    }
}
