using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _pauseButton;

    private void Awake()
    {
        _pauseButton.onClick.RemoveAllListeners();
        _pauseButton.onClick.AddListener(() => GameManager.Instance.PauseGame());
    }
}
