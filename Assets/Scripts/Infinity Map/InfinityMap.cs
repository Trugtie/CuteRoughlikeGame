using System;
using UnityEngine;

public class InfinityMap : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject _mapChunkPrefab;

    [Header(" Settings ")]
    [SerializeField] private float _mapChunkSize;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                GenerateMapChunk(x, y);
    }

    private void GenerateMapChunk(int x, int y)
    {
        Vector2 position = new Vector2(x, y) * _mapChunkSize;
        Instantiate(_mapChunkPrefab, position, Quaternion.identity, transform);
    }
}
