using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;

    private void Awake()
    {
        _resumeButton.onClick.RemoveAllListeners();
        _resumeButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
