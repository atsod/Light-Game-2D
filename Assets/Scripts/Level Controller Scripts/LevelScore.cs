using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevelScoreText;
    [SerializeField] private TextMeshProUGUI _maxLevelScoreText;

    private const string KEY_RECORD = PlayerPrefsKeys.KEY_RECORD;
    private int _score;

    private void OnEnable()
    {
        LevelController.StopLevel += OnDeadPlayer;
    }

    private void OnDisable()
    {
        LevelController.StopLevel -= OnDeadPlayer;
    }

    private void Awake()
    {
        _score = 0;

        _currentLevelScoreText.text = $"—чет: {_score}";
        _maxLevelScoreText.text = $"Ћучший счет: {PlayerPrefs.GetInt(KEY_RECORD, 0)}";
    }

    void Start()
    {
        StartCoroutine(IncreaseScoreInSeconds(3f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Finish>() != null)
        {
            _score += 10;
            _currentLevelScoreText.text = $"—чет: {_score}";

            CheckForMaxRecord();
        }
    }

    private IEnumerator IncreaseScoreInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _score += 5;
        _currentLevelScoreText.text = $"—чет: {_score}";
        
        CheckForMaxRecord();

        StartCoroutine(IncreaseScoreInSeconds(seconds));
    }

    private void CheckForMaxRecord()
    {
        if (_score > PlayerPrefs.GetInt(KEY_RECORD))
        {
            PlayerPrefs.SetInt(KEY_RECORD, _score);
            _maxLevelScoreText.text = $"Ћучший счет: {_score}";
        }
    }

    private void OnDeadPlayer()
    {
        StopAllCoroutines();
    }
}
