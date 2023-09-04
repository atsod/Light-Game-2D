using System;
using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static event Action StopLevel;

    public static float EnemyDownSpeed;

    private void Awake()
    {
        EnemyDownSpeed = 5f;
    }

    private void Start()
    {
        StartCoroutine(IncreaseEnemySpeedInSeconds(5f));
    }

    private IEnumerator IncreaseEnemySpeedInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (EnemyDownSpeed < 10f)
        {
            EnemyDownSpeed += 0.2f;
        }

        Debug.Log($"enemy down speed: {EnemyDownSpeed}");
        StartCoroutine(IncreaseEnemySpeedInSeconds(seconds));
    }

    public static void StopLevelTime()
    {
        StopLevel?.Invoke();
    }
}
