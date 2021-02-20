using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _tapForce = 250;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // задаем углы поворота персонажа по оси Z
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        ResetBird();
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && Time.timeScale != 0)
        {
            _animator.SetTrigger("Fly");
            _rigidbody.velocity = new Vector2(_speed, 0); // при тапе обнуляем скорость персонажа по оси Y и задаем скорость по оси X (не нужно гасить скорость при падении персонажа)
            transform.rotation = _maxRotation; // при тапе максимально задираем голову персонажа вверх
            _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime); // плавно возвращаем голову персонажа при падении вниз
    }

    public void ResetBird() // сбрасываем нашего персонажа при смерти
    {
        _animator.SetBool("Die", false);
        transform.position = _startPosition; // задаем стартовую позицию персонажа
        transform.rotation = Quaternion.Euler(0,0,0); // сбрасываем поворот персонажа
        _rigidbody.velocity = Vector2.zero; // обнуляем скорость персонажа в начале игры
    }
}
