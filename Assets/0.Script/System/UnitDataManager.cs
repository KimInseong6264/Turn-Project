using System.Collections.Generic;
using UnityEngine;

public class UnitDataManager : MonoBehaviour
{
    [SerializeField] private List<UnitDataSO> _unitDataList;

    private Dictionary<string, UnitDataSO> _unitDataDict;
    
    protected void Awake()
    {
        _unitDataDict = new Dictionary<string, UnitDataSO>();
        SetUnitData();
    }

    // 유닛Dict에 데이터를 세이브
    private void SetUnitData()
    {
        foreach (var unitData in _unitDataList)
        {
            SetPrefabUnitDat(unitData);
            
            _unitDataDict.TryAdd(unitData.Name, unitData);
        }
    }

    // SO의 프리펩UnitView에 SO데이터를 끼워넣음
    private void SetPrefabUnitDat(UnitDataSO unitData)
    {
        if (unitData.UnitPrefab == null)
            Debug.LogWarning($"{unitData.name}의 SO에 프리팹 없음");
        else
            unitData.UnitPrefab.GetComponent<UnitView>().unitData = unitData;
    }
    
    
    public UnitDataSO GetUnitData(string unitName) => _unitDataDict[unitName];
}