using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy _candyPrefab;
    [SerializeField] private Cash _cashPrefab;
    [SerializeField] private Chest _chestPrefab;

    private ObjectPool<Candy> _candyPool;
    private ObjectPool<Cash> _cashPool;
    private ObjectPool<Chest> _chestPool;

    [Header("Setting")]
    [SerializeField][Range(0, 100)] private int _cashDropChance;
    [SerializeField][Range(0, 100)] private int _chestDropChance;

    private void Awake()
    {
        _candyPool = new ObjectPool<Candy>(CreateCandyAction, GetCandyAction, ReleaseCandyAction, DestroyCandyAction);
        _cashPool = new ObjectPool<Cash>(CreateCashAction, GetCashAction, ReleaseCashAction, DestroyCashAction);
        _chestPool = new ObjectPool<Chest>(CreateChestAction, GetChestAction, ReleaseChestAction, DestroyChestAction);
    }

    private void DestroyCandyAction(Candy candy) => Destroy(candy.gameObject);
    private void ReleaseCandyAction(Candy candy) => candy.gameObject.SetActive(false);
    private void GetCandyAction(Candy candy) => candy.gameObject.SetActive(true);
    private Candy CreateCandyAction() => Instantiate(_candyPrefab, transform);

    private void DestroyCashAction(Cash cash) => Destroy(cash.gameObject);
    private void ReleaseCashAction(Cash cash) => cash.gameObject.SetActive(false);
    private void GetCashAction(Cash cash) => cash.gameObject.SetActive(true);
    private Cash CreateCashAction() => Instantiate(_cashPrefab, transform);

    private void DestroyChestAction(Chest chest) => Destroy(chest.gameObject);
    private void ReleaseChestAction(Chest chest) => chest.gameObject.SetActive(false);
    private void GetChestAction(Chest chest) => chest.gameObject.SetActive(true);
    private Chest CreateChestAction() => Instantiate(_chestPrefab, transform);

    private void Start()
    {
        Enemy.OnAnyPassAway += EnemyPassAwayCallBack;
        Enemy.OnBossPassAway += OnBossPassAwayCallback;
        Cash.OnAnyCashCollected += CashReleaseCallback;
        Candy.OnAnyCandyCollected += CandyReleaseCallback;
        Chest.OnAnyChestCollected += ChestReleaseCallback;
    }

    private void OnDestroy()
    {
        Enemy.OnAnyPassAway -= EnemyPassAwayCallBack;
        Enemy.OnBossPassAway += OnBossPassAwayCallback;
        Cash.OnAnyCashCollected -= CashReleaseCallback;
        Candy.OnAnyCandyCollected -= CandyReleaseCallback;
        Chest.OnAnyChestCollected -= ChestReleaseCallback;
    }

    private void ChestReleaseCallback(Chest chest)
    {
        _chestPool.Release(chest);
    }

    private void EnemyPassAwayCallBack(Vector2 dropPosition)
    {
        int random = Random.Range(0, 101);

        bool isDropCast = random < _cashDropChance;

        bool isDropedChest = TryGetChest(dropPosition);

        if (isDropedChest)
            return;

        if (isDropCast)
        {
            Cash cash = _cashPool.Get();
            cash.transform.position = dropPosition;
        }
        else
        {
            Candy candy = _candyPool.Get();
            candy.transform.position = dropPosition;
        }
    }

    private void OnBossPassAwayCallback(Vector2 dropPosition)
    {
        DropChest(dropPosition);
    }

    private bool TryGetChest(Vector2 dropPosition)
    {
        int random = Random.Range(0, 101);

        bool isDropChest = random < _chestDropChance;

        if (!isDropChest)
            return false;

        DropChest(dropPosition);

        return true;
    }

    private void DropChest(Vector2 dropPosition)
    {
        Chest chest = _chestPool.Get();
        chest.transform.position = dropPosition;
    }

    private void CandyReleaseCallback(Candy candy)
    {
        _candyPool.Release(candy);
    }

    private void CashReleaseCallback(Cash cash)
    {
        _cashPool.Release(cash);
    }


}
