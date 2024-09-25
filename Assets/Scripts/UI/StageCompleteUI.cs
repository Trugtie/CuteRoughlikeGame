using UnityEngine;
using UnityEngine.UI;

public class StageCompleteUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartGame();
        });
    }
}
