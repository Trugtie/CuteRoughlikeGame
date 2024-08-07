using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy _candyPrefab;
    [SerializeField] private Cash _cashPrefab;

    private void Start()
    {
        Enemy.OnAnyPassAway += EnemyPassAwayCallBack;
    }

    private void OnDestroy()
    {
        Enemy.OnAnyPassAway -= EnemyPassAwayCallBack;
    }

    private void EnemyPassAwayCallBack(Vector2 vector)
    {
        int random = Random.Range(0, 101);

        CurrencyDrop collectablePrefab = random < 50 ? _cashPrefab : _candyPrefab;

        Instantiate(collectablePrefab, vector, Quaternion.identity, transform);
    }
}
