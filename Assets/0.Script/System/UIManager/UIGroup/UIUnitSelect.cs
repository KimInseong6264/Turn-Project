using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIUnitSelect : UIGroup
{
    private List<ClickUnitSelect> _buttons;
    private UnityObjectPull<ClickUnitSelect> _unitSelectPull;
    
    [SerializeField] private Transform _selectListTransform;
    [SerializeField] private ClickUnitSelect _unitSelectPrefab;
    
    protected override void Awake()
    {
        _unitSelectPull = new UnityObjectPull<ClickUnitSelect>(_unitSelectPrefab, 5, _selectListTransform);
        base.Awake();
    }

    private void Start()
    {
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
            SetButton(obj, unitData.Value);
            _buttons.Add(obj);
        }
    }

    // 버튼의 기능들을 설정
    private void SetButton(ClickUnitSelect unitSelect, UnitDataSO unitData)
    {
        unitSelect.OnClick += () => UnitSelector.SelectedUnit(unitData.Name);
        unitSelect.gameObject.name = unitData.name;
        unitSelect.GetComponentInChildren<Text>().text = unitData.name;
    }
}