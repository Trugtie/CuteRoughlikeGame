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
    public void PlayAnim(int damge, bool isCriticalHit)
    {
        _damgeText.color = isCriticalHit ? Color.yellow : Color.white;
        _damgeText.SetText(damge.ToString());
        _animator.Play("DamgeTextFloatUp");
    }
}
