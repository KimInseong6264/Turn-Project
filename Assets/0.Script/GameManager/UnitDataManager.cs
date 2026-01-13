using System.Collections.Generic;
using UnityEngine;

public class UnitDataManager : SingletonBase<UnitDataManager>
{
    [SerializeField] private List<UnitDataSO> _unitDataList;

    private Dictionary<string, UnitDataSO> _unitDataDict;
    
    protected override void Awake()
    {
        base.Awake();
        _unitDataDict = new Dictionary<string, UnitDataSO>();
        SetUnitData();
    }

    private void SetUnitData()
    {
        foreach (var unitData in _unitDataList)
        {
            if (!_unitDataDict.TryGetValue(unitData.Name, out UnitDataSO unitData2))
                _unitDataDict.Add(unitData.Name, unitData);
        }
    }
    
    public Unit OnCreateUnit(string unitName)
    {
        if (!_unitDataDict.TryGetValue(unitName, out UnitDataSO unitData))
        {
            Debug.LogError($"Unit {unitName} 데이터를 찾지 못함");
        }
        
        GameObject prefab = _unitDataDict[unitName].UnitPrefab.gameObject;
        Unit unit = Instantiate(prefab).GetComponent<Unit>();

        return unit;
    }
}