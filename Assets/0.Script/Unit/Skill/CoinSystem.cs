using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinSystem : MonoBehaviour
{
    private List<Coin> _ativeCoins;
    private ObjectPull<Coin> _coinPull;
    private int _coinValueSum;
    
    [SerializeField] private Coin _coinPrefab;

    public int CoinValueSum => _coinValueSum;
    public event Action OnNotifyCoinToss;
    
    private void Awake()
    {
        _ativeCoins = new List<Coin>();
        _coinPull = new ObjectPull<Coin>(_coinPrefab, 15, transform);
    }

    private void OnDisable()
    {
        Init();
    }

    // 코인토스 실행
    public IEnumerator OnCoinToss(int coinCount, int coinValue, float startDelay, float tossFrequency)
    {
        Init();
        
        _coinPull.GetPull(_ativeCoins, coinCount);
        yield return CoroutineManager.GetWaitTime(startDelay);

        foreach (var coin in _ativeCoins)
        {
            var result = coin.GetTossResult();
            _coinValueSum += (int)result * coinValue;
            OnNotifyCoinToss?.Invoke();
            yield return CoroutineManager.GetWaitTime(tossFrequency);
        }
    }

    private void Init()
    {
        foreach (var ativeCoin in _ativeCoins)
        {
            _coinPull.Release(ativeCoin);
        }
        _ativeCoins.Clear();
        _coinValueSum = 0;
    }
}