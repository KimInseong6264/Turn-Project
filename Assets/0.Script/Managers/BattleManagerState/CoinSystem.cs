using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다른 클래스에서는 코인 시스템을 new로 받아오기만 하면 됨
public class CoinSystem
{
    // private UnityObjectPull<>
    public List<CoinType> TossResult { get; private set; } = new();

    // 코인토스가 모두 끝나면 True
    public bool IsDone { get; private set; }


    public CoinType GetCoinToss()
    {
        CoinType random = (CoinType)Random.Range(0, 2);
        TossResult.Add(random);

        return random;
    }

    public void GetCoinReset()
    {
        TossResult.Clear();
        IsDone = false;
    }
}

public enum CoinType
{
    Back, Front
}