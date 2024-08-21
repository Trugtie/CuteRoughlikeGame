using UnityEngine.UI;
using UnityEngine;

public class WaveTransitionUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _goShopButton;

    private void Start()
    {
        _goShopButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetState(GameStates.SHOP);
        });
    }
}
