using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartGame();
        });
    }
}
