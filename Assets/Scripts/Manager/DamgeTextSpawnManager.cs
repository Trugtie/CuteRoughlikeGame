using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class DamgeTextSpawnManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamgeText _damgeText;
    private ObjectPool<DamgeText> _damgeTextPool;

    private void Awake()
    {
        _damgeTextPool = new ObjectPool<DamgeText>(CreateDamgeText, OnGetDamgeText, OnRealeaseDamgeText, OnDestroyDamgeText);
    }

    private DamgeText CreateDamgeText()
    {
        return Instantiate(_damgeText, transform);
    }

    private void OnGetDamgeText(DamgeText damgeText)
    {
        damgeText.gameObject.SetActive(true);
    }

    private void OnRealeaseDamgeText(DamgeText damgeText)
    {
        damgeText.gameObject.SetActive(false);
    }


    private void OnDestroyDamgeText(DamgeText damgeText)
    {
        Destroy(damgeText.gameObject);
    }

    private void Start()
    {
        Enemy.OnAnyHit += HitCallpack;
    }

    private void HitCallpack(Vector2 targetPosition, int damge)
    {
        DamgeText damgeTextInstance = _damgeTextPool.Get();
        damgeTextInstance.transform.position = targetPosition;
        damgeTextInstance.PlayAnim(damge);
        LeanTween.delayedCall(1f, () => { _damgeTextPool.Release(damgeTextInstance); });
    }
}
