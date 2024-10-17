using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _sfxButton;
    [SerializeField] private Button _musicButton;

    [Header(" Settings ")]
    [SerializeField] private Color _onButtonColor;
    [SerializeField] private Color _offButtonColor;

    private void Awake()
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(Hide);

        _sfxButton.onClick.RemoveAllListeners();
        _sfxButton.onClick.AddListener(sfxButtonClickCallback);

        _musicButton.onClick.RemoveAllListeners();
        _musicButton.onClick.AddListener(musicButtonClickCallback);

    }

    private void Start()
    {
        InitSetting();
    }

    private void musicButtonClickCallback()
    {
        SettingManager.Instance.SetMusicState();
        ChangeStateToggleButton(_musicButton, SettingManager.Instance.MusicState);
    }

    private void sfxButtonClickCallback()
    {
        SettingManager.Instance.SetSFXState();
        ChangeStateToggleButton(_sfxButton, SettingManager.Instance.SFXState);
    }

    private void InitSetting()
    {
        ChangeStateToggleButton(_musicButton, SettingManager.Instance.MusicState);
        ChangeStateToggleButton(_sfxButton, SettingManager.Instance.SFXState);
    }

    private void ChangeStateToggleButton(Button button, bool isButtonOnState)
    {
        if (isButtonOnState)
        {
            button.GetComponent<Image>().color = _onButtonColor;
            button.GetComponentInChildren<TextMeshProUGUI>().SetText("ON");
        }
        else
        {
            button.GetComponent<Image>().color = _offButtonColor;
            button.GetComponentInChildren<TextMeshProUGUI>().SetText("OFF");
        }
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
