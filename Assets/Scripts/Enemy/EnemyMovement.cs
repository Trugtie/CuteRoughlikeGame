using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player _player;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        if (_player != null)
            FollowPlayer();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }
}
