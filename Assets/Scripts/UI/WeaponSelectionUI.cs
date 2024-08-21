using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _doneButton;

    private void Start()
    {
        _doneButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetState(GameStates.GAMEPLAY);
        });
    }
}
