using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelColor", menuName = ("LevelColor/New LevelColor"), order = 0)]
public class ColorPalleteSO : ScriptableObject
{
    [field: SerializeField] public Color[] LevelColors;
    [field: SerializeField] public Color[] LevelOulineColors;
}
