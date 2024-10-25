using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerVisual : MonoBehaviour
{
    private const string SPEED = "speed";

    [Header(" Elements ")]
    private SpriteRenderer _playerRenderer;
    private Animator _animator;

    private void Awake()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(SPEED, PlayerController.Instance.PlayerRbVelocityMagnitude);
        _animator.speed = PlayerController.Instance.MoveSpeed;
    }

    private void Start()
    {
        CharacterSelectionManager.OnSelectedCharacterAction += OnSelectedCharacterActionCallback;
    }

    private void OnDestroy()
    {
        CharacterSelectionManager.OnSelectedCharacterAction -= OnSelectedCharacterActionCallback;
    }

    private void OnSelectedCharacterActionCallback(CharacterDataSO characterDataSO)
    {
        _playerRenderer.sprite = characterDataSO.CharacterSprite;
    }
}
