using UnityEngine.UI;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _startGameBtn;

    private void Start()
    {
        _startGameBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SetState(GameStates.GAMEPLAY);
        });
    }
}
