using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Slider _expSlider;
    [SerializeField] private TextMeshProUGUI _expText;
    private PlayerExp _playerExp;

    private void Awake()
    {
        _playerExp = FindObjectOfType<PlayerExp>();
        UpdateVisual();
    }

    void Start()
    {
        _playerExp.OnGainExp += UpdateVisual;
        _playerExp.OnLevelUp += UpdateVisual;
    }

    private void OnDestroy()
    {
        _playerExp.OnGainExp -= UpdateVisual;
        _playerExp.OnLevelUp -= UpdateVisual;

    }

    private void UpdateVisual()
    {
        _expSlider.value = _playerExp.GetExpValue();
        _expText.text = "Level " + _playerExp.GetCurrentLevel().ToString();
    }
}
