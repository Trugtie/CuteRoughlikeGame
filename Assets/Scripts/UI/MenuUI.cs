using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _startGameBtn;

    private void Start()
    {
        _startGameBtn.onClick.AddListener(GameManager.Instance.StartGame);
    }
}
