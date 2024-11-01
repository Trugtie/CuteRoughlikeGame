using System;
using UnityEngine;

public class InifinityChildMover : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _playerTransform;

    [Header(" Settings ")]
    [SerializeField] private float _mapChunkSize;
    [SerializeField] private float _distanceThreshold = 1.5f;

    private void Update()
    {
        UpdateChildPosition();
    }

    private void UpdateChildPosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            Vector3 distance = _playerTransform.position - child.position;
            float calculatedDistanceThreshold = _distanceThreshold * _mapChunkSize;

            if (MathF.Abs(distance.x) > calculatedDistanceThreshold)
                child.position += Vector3.right * calculatedDistanceThreshold * 2 * MathF.Sign(distance.x);

            if (MathF.Abs(distance.y) > calculatedDistanceThreshold)
                child.position += Vector3.up * calculatedDistanceThreshold * 2 * MathF.Sign(distance.y);
        }
    }
}
