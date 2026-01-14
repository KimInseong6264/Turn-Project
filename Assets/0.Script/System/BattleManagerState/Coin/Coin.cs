using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다른 클래스에서는 코인 시스템을 new로 받아오기만 하면 됨
public class Coin : MonoBehaviour
{
    private CoinType _coinType;
    
    public void SetCoinType(CoinType coinType) => _coinType = coinType;
    
}