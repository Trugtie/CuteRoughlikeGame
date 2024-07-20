using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player _player;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;

    private void Start()
    {
        _player = FindFirstObjectByType<Player>();

        if (_player == null)
        {
            Debug.LogWarning("No player in game !");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }
}
