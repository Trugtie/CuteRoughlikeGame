using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _characterSelectionBtn;
    [SerializeField] private Button _settingBtn;
    [SerializeField] private CharacterSelectionUI _characterSelectionUI;
    [SerializeField] private Image _characterIcon;
    [SerializeField] private SettingUI _settingUI;

    private void Start()
    {
        _startGameBtn.onClick.RemoveAllListeners();
        _startGameBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SetState(GameStates.WEAPONSELECTION);
        });

        _characterSelectionBtn.onClick.RemoveAllListeners();
        _characterSelectionBtn.onClick.AddListener(() => _characterSelectionUI.Show());

        _settingBtn.onClick.RemoveAllListeners();
        _settingBtn.onClick.AddListener(() => _settingUI.Show());

        CharacterSelectionManager.OnSelectedCharacterAction += OnSelectedCharacterActionCallback;
    }

    private void OnDestroy()
    {
        CharacterSelectionManager.OnSelectedCharacterAction -= OnSelectedCharacterActionCallback;
    }

    private void OnSelectedCharacterActionCallback(CharacterDataSO characterDataSO)
    {
        _characterIcon.sprite = characterDataSO.CharacterSprite;
    }
}
