using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Transform _characterScrollContentParent;
    [SerializeField] private CharacterButtonUI _characterButtonUIPrefab;

    private CharacterDataSO[] _charactersDataSO;

    private void Awake()
    {
        _charactersDataSO = ResourceManager.Characters;

        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        InitializeCharacterScrollContent();
    }

    private void InitializeCharacterScrollContent()
    {
        if (_charactersDataSO.Length <= 0)
        {
            Debug.LogError("None characters!");
            return;
        }

        ClearCharacterButtonScroll();

        for (int i = 0; i < _charactersDataSO.Length; i++)
        {
            CharacterButtonUI characterButtonInstance = Instantiate(_characterButtonUIPrefab, _characterScrollContentParent);
            characterButtonInstance.Configure(_charactersDataSO[i], i);
        }
    }

    private void ClearCharacterButtonScroll()
    {
        foreach (Transform child in _characterScrollContentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
