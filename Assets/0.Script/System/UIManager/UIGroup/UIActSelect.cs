using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIActSelect : UIGroup
{
    private List<ClickUnitSelect> _buttons;
    private UnityObjectPull<ClickUnitSelect> _unitSelectPull;
    
    [SerializeField] private Transform _selectListTransform;
    [SerializeField] private ClickUnitSelect _skillSelectPrefab;
    
    protected override void Awake()
    {
        base.Awake();
        _unitSelectPull = new UnityObjectPull<ClickUnitSelect>(_skillSelectPrefab, 5, _selectListTransform);
    }

    private void Start()
    {
        OnOpen();
    }

    // 유니티에 생성시
    protected override void OnOpen()
    {
        base.OnOpen();
        
        if(_buttons == null)
            OnCreateButton();
    }
    
    // 게임 시작시, 버튼 생성 메서드
    public void OnCreateButton()
    {
        _buttons = new List<ClickUnitSelect>();
        
        Dictionary<string, UnitDataSO> unitDict = GameManager.Instance.GetUnitDataList();
        foreach (var unitData in unitDict)
        {
            var obj = _unitSelectPull.GetPull();
            obj.gameObject.name = unitData.Key;
            _buttons.Add(obj);
        }
    }
}