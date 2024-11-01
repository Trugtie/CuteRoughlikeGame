using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform _target;

    [Header("Settings")]
    [SerializeField] private float _zOffset;
    [SerializeField] private Vector2 _clampMinMaxXY;

    private void LateUpdate()
    {
        Vector3 targetPos = _target.position;
        targetPos.z = _zOffset;

        if (!GameManager.Instance.IsUseInfinityMap)
        {
            targetPos.x = Mathf.Clamp(targetPos.x, -_clampMinMaxXY.x, _clampMinMaxXY.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -_clampMinMaxXY.y, _clampMinMaxXY.y);
        }

        transform.position = targetPos;
    }
}
