using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CredistPanelUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private Button _backButton;

    [Header(" Settings ")]
    [SerializeField] private float _scrollSpeed;

    private void Awake()
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        Hide();
    }

    private void OnEnable()
    {
        _contentTransform.anchoredPosition = new Vector2(_contentTransform.anchoredPosition.x, 0);
    }

    private void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        _contentTransform.anchoredPosition += Vector2.up * _scrollSpeed * Time.deltaTime;
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
