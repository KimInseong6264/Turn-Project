using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// 다른 클래스에서는 코인 시스템을 new로 받아오기만 하면 됨
public class CoinSystem : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    private UnityObjectPull<Coin> _coinPull;
    
    public List<Coin> Coins { get; private set; }
    
    public List<CoinType> TossResult { get; private set; }

    private void Awake()
    {
        _coinPull = new UnityObjectPull<Coin>(_coinPrefab, 30, transform);
        TossResult = new List<CoinType>();
        Coins = new List<Coin>();
    }

    public void StartCoinToss(int coinCount) => StartCoroutine(CoinTossRoutine((coinCount)));
    
    // 코인토스 코루틴
    private IEnumerator CoinTossRoutine(int coinCount)
    {
        TossResult.Clear();
        CreateCoin(coinCount);
        
        foreach (var coin in Coins)
        {
            yield return CoroutineManager.GetWaitTime(1f);
            
            CoinType coinType = OnCoinToss();
            TossResult.Add(coinType);
            coin.SetCoinType(coinType);

            Debug.Log(coinType);
        }
        Debug.Log("======================");
        
        yield return CoroutineManager.GetWaitTime(1f);
        RealeseCoin();
    }

    // 코인을 풀에서 꺼내오기
    private void CreateCoin(int coinCount)
    {
        Coins.Clear();
        
        for (int i = 0; i < coinCount; i++)
        {
            Coins.Add(_coinPull.GetPull());
        }
    }

    // 코인을 다시 풀에 넣기
    private void RealeseCoin()
    {
        foreach (var coin in Coins)
        {
            _coinPull.Release(coin);
        }
    }
    
    private CoinType OnCoinToss() => (CoinType)Random.Range(0, 2);
}

public enum CoinType
{
    Back, Front
}