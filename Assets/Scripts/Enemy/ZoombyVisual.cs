using System;
using UnityEngine;

public class ZoombyVisual : MonoBehaviour
{
    private const string IDLE = "Idle";
    private const string ATTACK = "Attack";
    private const string MOVE = "Move";

    [Header(" Elements ")]
    [SerializeField] private Zoomby _zoomby;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _zoomby.OnStartIdle += OnStartIdleCallback;
        _zoomby.OnStartAttacking += OnStartAttackingCallback;
        _zoomby.OnStartMoving += OnStartMovingCallback;
    }

    private void OnStartIdleCallback()
    {
        PlayAnim(IDLE);
    }

    private void OnStartAttackingCallback()
    {
        PlayAnim(ATTACK);
    }

    private void OnStartMovingCallback()
    {
        PlayAnim(MOVE);
    }

    public void ResetStateAnimEvent()
    {
        _zoomby.StartIdleState();
    }

    public void AttackAnimEvent()
    {
        _zoomby.Attack();
    }

    private void PlayAnim(string stateString)
    {
        _animator.Play(stateString);
    }
}
