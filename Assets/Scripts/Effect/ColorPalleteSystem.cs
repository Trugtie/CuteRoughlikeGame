using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalleteSystem : MonoBehaviour
{
    [SerializeField] private ColorPalleteSO _colorPalleteSO;

    public static ColorPalleteSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Color GetLevelColor(int level)
    {
        level = Mathf.Clamp(level, 0, _colorPalleteSO.LevelColors.Length);

        return _colorPalleteSO.LevelColors[level];
    }

    public Color GetLevelOulineColor(int level)
    {
        level = Mathf.Clamp(level, 0, _colorPalleteSO.LevelOulineColors.Length);

        return _colorPalleteSO.LevelOulineColors[level];
    }
}
