using System.Collections;
using UnityEngine;

public abstract class CurrencyDrop : MonoBehaviour, ICollectable
{
    [Header("Settings")]
    private bool _isCollected;

    private void OnEnable()
    {
        _isCollected = false;
    }

    public void Collect(Player player)
    {
        if (_isCollected)
            return;

        _isCollected = true;

        StartCoroutine(MoveToPlayerRoutine(player));
    }

    private IEnumerator MoveToPlayerRoutine(Player player)
    {
        float timer = 0;

        Vector2 startPosition = transform.position;

        while (timer < 1)
        {
            Vector2 targetPosition = player.GetCenterPoint();
            transform.position = Vector2.Lerp(startPosition, targetPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        Collected();

    }

    protected abstract void Collected();
}
