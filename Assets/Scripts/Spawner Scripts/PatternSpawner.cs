using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _patternsList;
    [SerializeField] private float _spawnInSeconds;

    private Vector3 _spawnPos;

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
        _spawnPos = transform.position - new Vector3(21f, 0, 0);
    }

    private void Start()
    {
        StartCoroutine(SpawnPatternInSeconds(_spawnInSeconds, 0));
    }

    private IEnumerator SpawnPatternInSeconds(float seconds, int exclusiveIndex)
    {
        int randomIndex = CustomRandomRange(exclusiveIndex);

        Instantiate(
            _patternsList[randomIndex], 
            _spawnPos, 
            Quaternion.identity);

        yield return new WaitForSeconds(seconds);

        if(_spawnInSeconds > 2f) _spawnInSeconds -= 0.05f;
        Debug.Log($"spawn in seconds: {_spawnInSeconds}");

        StartCoroutine(SpawnPatternInSeconds(_spawnInSeconds, randomIndex));
    }

    private int CustomRandomRange(int exclusiveIndex)
    {
        int temp = Random.Range(0, _patternsList.Capacity);

        if (temp == exclusiveIndex) 
            temp = CustomRandomRange(exclusiveIndex);

        return temp;
    }

    private void OnDeadPlayer()
    {
        StopAllCoroutines();
    }
}
