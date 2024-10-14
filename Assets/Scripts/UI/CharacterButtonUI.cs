using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _characterIcon;
    [SerializeField] private Image _lockIcon;

    public Button CharacterButton { get; private set; }

    public int Index { get; private set; }

    private void Awake()
    {
        CharacterButton = GetComponentInChildren<Button>();
    }

    public void Configure(CharacterDataSO characterDataSO, int index, bool isUnLocked)
    {
        _characterIcon.sprite = characterDataSO.CharacterSprite;
        Index = index;

        if (!isUnLocked)
            Lock();
        else
            Unlock();

    }

    public void Lock()
    {
        _lockIcon.gameObject.SetActive(true);
        _characterIcon.color = Color.gray;
    }

    public void Unlock()
    {
        _lockIcon.gameObject.SetActive(false);
        _characterIcon.color = Color.white;
    }
}
