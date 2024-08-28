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

    private void Start()
    {
        Enemy.OnAnyHit += HitCallpack;
        PlayerHealth.OnDodged += DogdedCallback;
    }

    private void OnDestroy()
    {
        Enemy.OnAnyHit -= HitCallpack;
        PlayerHealth.OnDodged -= DogdedCallback;
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

    private void HitCallpack(Vector2 targetPosition, int damge, bool isCriticalHit)
    {
        DamgeText damgeTextInstance = _damgeTextPool.Get();
        damgeTextInstance.transform.position = targetPosition;
        damgeTextInstance.PlayAnim(damge.ToString(), isCriticalHit);
        LeanTween.delayedCall(1f, () => { _damgeTextPool.Release(damgeTextInstance); });
    }

    private void DogdedCallback(Vector3 dogedPosition)
    {
        DamgeText damgeTextInstance = _damgeTextPool.Get();
        damgeTextInstance.transform.position = dogedPosition;
        damgeTextInstance.PlayAnim("Dodged", false);
        LeanTween.delayedCall(1f, () => { _damgeTextPool.Release(damgeTextInstance); });
    }
}
