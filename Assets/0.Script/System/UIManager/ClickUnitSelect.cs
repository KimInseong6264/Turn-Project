using System.Collections.Generic;
using UnityEngine;


public class ClickUnitSelect : ClickObject
{
    private List<UnitDataSO> _unitDataList;
    private static int _count;
    private string _unitName;

    private void Awake()
    {
        _unitDataList = new List<UnitDataSO>();
    }

    private void Start()
    {
        foreach (var unitDataSO in GameManager.Instance.GetUnitDataList())
        {
            Debug.Log("유닛베이스" + unitDataSO);
            _unitDataList.Add(unitDataSO.Value);
        }

        _unitName = _unitDataList[_count++].Name;
        gameObject.name = _unitName;
    }

    public override void OnCklick()
    {
        Debug.Log("클릭실행" + gameObject.name);
        UnitSelector.SelectedUnit(_unitName);
    }
}