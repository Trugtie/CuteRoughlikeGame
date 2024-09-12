using UnityEngine.UI;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _startGameBtn;
    [SerializeField] private Button _rerollButton;

    private void Start()
    {
        _startGameBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SetState(GameStates.GAMEPLAY);
        });

        _rerollButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.RerollShopItem();
        });
    }
}
