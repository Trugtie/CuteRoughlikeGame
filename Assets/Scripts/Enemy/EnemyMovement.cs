using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player _player;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void FollowPlayer()
    {
        if (_player != null)
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }
}
