using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _sfxButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _policyButton;
    [SerializeField] private Button _helpButton;

    [Header(" Settings ")]
    [SerializeField] private Color _onButtonColor;
    [SerializeField] private Color _offButtonColor;

    private void Awake()
    {
        SetupButton();
    }

    private void Start()
    {
        InitSetting();
    }

    private void SetupButton()
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(Hide);

        _sfxButton.onClick.RemoveAllListeners();
        _sfxButton.onClick.AddListener(sfxButtonClickCallback);

        _musicButton.onClick.RemoveAllListeners();
        _musicButton.onClick.AddListener(musicButtonClickCallback);

        _policyButton.onClick.RemoveAllListeners();
        _policyButton.onClick.AddListener(policyButtonClickCallback);

        _helpButton.onClick.RemoveAllListeners();
        _helpButton.onClick.AddListener(helpButtonClickCallback);
    }

    private void helpButtonClickCallback()
    {
        string email = "wongamedev@gmail.com";
        string subject = MyEscapeURL("Help!");
        string body = MyEscapeURL("Help me with this error...");

        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private string MyEscapeURL(string s)
    {
        return UnityWebRequest.EscapeURL(s).Replace("+", "%20");
    }

    private void policyButtonClickCallback()
    {
        Application.OpenURL("https://www.tiktok.com/@wongamedev");
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
