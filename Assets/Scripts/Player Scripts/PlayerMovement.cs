using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;

    private Rigidbody2D _rb;
    private TrailRenderer _trail;
    private Light2D _playerLight;

    private float _lightOuterRadius;
    private float _extraLightOuterRadius;
    private float _usualSpeed;
    private float _extraSpeed;

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
        _rb = GetComponent<Rigidbody2D>();
        _trail = GetComponentInChildren<TrailRenderer>();
        _playerLight = GetComponentInChildren<Light2D>();

        _lightOuterRadius = UpgradeSwitcher.playerLightOuterRadius;
        _extraLightOuterRadius = _lightOuterRadius * 0.75f;
        _usualSpeed = UpgradeSwitcher.usualPlayerSpeed;
        _extraSpeed = _usualSpeed * 1.3f;

        _playerLight.pointLightOuterRadius = _lightOuterRadius;
    }

    void Start()
    {
        StartCoroutine(SafeTeleportToSpawnInSeconds(0.3f));
    }

    private IEnumerator SafeTeleportToSpawnInSeconds(float seconds)
    {
        _trail.enabled = false;
        transform.position = _spawnPoint.transform.position;
        yield return new WaitForSeconds(seconds);
        _trail.enabled = true;
    }

    void FixedUpdate()
    {
        Move();
        ChangeLightInAcceleration();
    }

    private void Move()
    {
        Vector2 moveInput = new(
            Input.GetAxisRaw("Horizontal") * 
            (Input.GetKey(KeyCode.LeftShift) 
                ? _extraSpeed 
                : _usualSpeed),
            0f);
        _rb.MovePosition(_rb.position + moveInput * Time.deltaTime);
    }

    private void ChangeLightInAcceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _playerLight.pointLightOuterRadius = _extraLightOuterRadius;
        else 
            _playerLight.pointLightOuterRadius = _lightOuterRadius;
    }

    private void OnDeadPlayer()
    {
        _usualSpeed = 0f;
        _extraSpeed = 0f;
        // Добавить анимацию увеличения радиуса света
        _lightOuterRadius = 20f;
        _extraLightOuterRadius = 20f;
    }
}