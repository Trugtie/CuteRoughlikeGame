using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float _shakeMagnitude;
    [SerializeField] private float _shakeDuration;

    private void Start()
    {
        RangeWeapon.OnAnyShoot += Shake;
    }

    private void OnDestroy()
    {
        RangeWeapon.OnAnyShoot -= Shake;
    }

    private void Shake()
    {
        transform.localPosition.Set(0, 0, -10f);

        Vector2 direction = Random.onUnitSphere.normalized;

        LeanTween.cancel(gameObject);
        LeanTween.moveLocal(gameObject, direction * _shakeMagnitude, _shakeDuration)
            .setEase(LeanTweenType.easeShake);
    }
}
