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
        Debug.Log("버튼이름" + _unitName);
    }

    public override void OnStartCklick()
    {
        Debug.Log("클릭실행" + gameObject.name);
        UnitSelector.SelectedUnit(_unitName);
    }
    
    public void SetUnitName(string unitName) => _unitName = unitName;
}