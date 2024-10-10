using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _characterIcon;

    public int Index { get; private set; }

    public void Configure(CharacterDataSO characterDataSO, int index)
    {
        _characterIcon.sprite = characterDataSO.CharacterSprite;
        Index = index;
    }
}
