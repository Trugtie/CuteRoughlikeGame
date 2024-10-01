using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartConfirmUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _nopeButton;
    [SerializeField] private Button _yupButton;

    private void Awake()
    {
        _nopeButton.onClick.RemoveAllListeners();
        _nopeButton.onClick.AddListener(() => Hide());

        _yupButton.onClick.RemoveAllListeners();
        _yupButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }

    private void Start()
    {
        Hide();
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
