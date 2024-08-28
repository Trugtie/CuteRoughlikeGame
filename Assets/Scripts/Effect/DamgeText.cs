using TMPro;
using UnityEngine;

public class DamgeText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro _damgeText;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [NaughtyAttributes.Button]
    public void PlayAnim(string damgeText, bool isCriticalHit)
    {
        _damgeText.color = isCriticalHit ? Color.yellow : Color.white;
        _damgeText.SetText(damgeText);
        _animator.Play("DamgeTextFloatUp");
    }
}
