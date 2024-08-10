using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy _candyPrefab;
    [SerializeField] private Cash _cashPrefab;
    private ObjectPool<Candy> _candyPool;
    private ObjectPool<Cash> _cashPool;

    private void Awake()
    {
        _candyPool = new ObjectPool<Candy>(CreateCandyAction, GetCandyAction, ReleaseCandyAction, DestroyCandyAction);
        _cashPool = new ObjectPool<Cash>(CreateCashAction, GetCashAction, ReleaseCashAction, DestroyCashAction);
    }

    private void DestroyCandyAction(Candy candy) => Destroy(candy.gameObject);
    private void ReleaseCandyAction(Candy candy) => candy.gameObject.SetActive(false);
    private void GetCandyAction(Candy candy) => candy.gameObject.SetActive(true);
    private Candy CreateCandyAction() => Instantiate(_candyPrefab, transform);

    private void DestroyCashAction(Cash cash) => Destroy(cash.gameObject);
    private void ReleaseCashAction(Cash cash) => cash.gameObject.SetActive(false);
    private void GetCashAction(Cash cash) => cash.gameObject.SetActive(true);
    private Cash CreateCashAction() => Instantiate(_cashPrefab, transform);

    private void Start()
    {
        Enemy.OnAnyPassAway += EnemyPassAwayCallBack;
        Cash.OnAnyCashCollected += CashReleaseCallback;
        Candy.OnAnyCandyCollected += CandyReleaseCallback;
    }

    private void OnDestroy()
    {
        Enemy.OnAnyPassAway -= EnemyPassAwayCallBack;
        Cash.OnAnyCashCollected -= CashReleaseCallback;
        Candy.OnAnyCandyCollected -= CandyReleaseCallback;
    }

    private void EnemyPassAwayCallBack(Vector2 vector)
    {
        int random = Random.Range(0, 101);

        bool isCash = random < 50 ? true : false;

        if (isCash)
        {
            Cash cash = _cashPool.Get();
            cash.transform.position = vector;
        }
        else
        {
            Candy candy = _candyPool.Get();
            candy.transform.position = vector;
        }
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
