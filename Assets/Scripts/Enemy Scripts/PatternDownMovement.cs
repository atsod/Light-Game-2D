using UnityEngine;

public class PatternDownMovement : MonoBehaviour
{
    private float _movementSpeed;
    private Transform _transform;

    private void OnEnable()
    {
        LevelController.StopLevel += OnDeadPlayer;
    }

    private void OnDisable()
    {
        LevelController.StopLevel -= OnDeadPlayer;
    }

    private void Start()
    {
        _movementSpeed = LevelController.EnemyDownSpeed;
        _transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        EnemyDownMove();
    }

    private void EnemyDownMove()
    {
        _transform.Translate(_movementSpeed * Time.deltaTime * Vector3.down);
    }

    private void OnDeadPlayer()
    {
        _movementSpeed = 0f;
    }
}
