using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header(" Elements ")]
    private SpriteRenderer _playerRenderer;

    private void Awake()
    {
        _playerRenderer = GetComponent<SpriteRenderer>();
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
