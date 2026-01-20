using System;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    private List<UnitDataSO> _enemyDataList;
        
    [field: SerializeField] public List<UnitDataSO> EnemyDataList { get; private set; }

    
    
    private void Awake()
    {
        _enemyDataList = new List<UnitDataSO>();
    }

    
    
    // 웨이브별 에너미 데이터 가져오기
    public List<UnitDataSO> GetEnemyDataList()
    {
        _enemyDataList.Clear();
        foreach (var enemyData in EnemyDataList)
        {
            _enemyDataList.Add(enemyData);
        }
        
        return _enemyDataList;
    }

    public void Init()
    {
        _enemyDataList.Clear();
    }
}