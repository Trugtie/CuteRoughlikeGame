using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _characterIcon;

    public Button CharacterButton { get; private set; }

    public int Index { get; private set; }

    private void Awake()
    {
        CharacterButton = GetComponentInChildren<Button>();
    }

    public void Configure(CharacterDataSO characterDataSO, int index)
    {
        _characterIcon.sprite = characterDataSO.CharacterSprite;
        Index = index;
    }
}
