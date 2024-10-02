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
    [SerializeField] private RestartConfirmUI _restartConfirmUI;

    private void Awake()
    {
        _resumeButton.onClick.RemoveAllListeners();
        _resumeButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());

        _menuButton.onClick.RemoveAllListeners();
        _menuButton.onClick.AddListener(() => _restartConfirmUI.Show());
    }

    public void Show()
    {
        Debug.Log("Pause show");
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Pause Hide");
        gameObject.SetActive(false);
    }
}
