using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(Animator))]

public class Bird : MonoBehaviour
{
    private BirdMover _mover;
    private Animator _animator;
    private int _score;

    public event UnityAction GameOver;
    public event UnityAction<int> ScoreChanged;

    private void Start()
    {
        _mover = GetComponent<BirdMover>();
        _animator = GetComponent<Animator>();
    }

    public void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void ResetPlayer()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.ResetBird();
    }

    public void Die()
    {
        _animator.SetBool("Die", true);
        GameOver?.Invoke(); // когда персонаж умер вызываем событие GameOver
    }
}
