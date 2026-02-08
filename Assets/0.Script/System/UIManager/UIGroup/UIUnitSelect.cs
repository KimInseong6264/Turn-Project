using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIUnitSelect : UIGroup
{
    private List<ClickUnitSelect> _buttons;
    private ObjectPull<ClickUnitSelect> _unitSelectPull;
    
    [SerializeField] private ClickUnitSelect _unitSelectPrefab;
    [SerializeField] private Button _battleStartButton; 
    [SerializeField] private Transform _buttonParent;
    
    
    
    protected override void Awake()
    {
        _buttons = new List<ClickUnitSelect>();
        _unitSelectPull = new ObjectPull<ClickUnitSelect>(_unitSelectPrefab, 5, _buttonParent);
        base.Awake();
    }

    private void Update()
    {
        if (UnitSelector.Players.Count == 0)
        {
            _battleStartButton.interactable = false;
            return;
        }
        _battleStartButton.interactable = true;
    }

    private void OnEnable()
    {
        OnCreateButton();
        GameManager.Instance.UpdateUI(UIGroupName.GameStart, false);
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            _unitSelectPull.Release(button);
        }
        _buttons.Clear();
    }


    // 게임 시작시, 버튼 생성 메서드
    public void OnCreateButton()
    {
        _buttons.Clear();
        
        Dictionary<string, UnitDataSO> unitDict = GameManager.Instance.GetUnitDataList();
        foreach (var unitData in unitDict.Values)
        {
            if(unitData.Team == UnitTeam.Enemy)
                continue;

            var obj = _unitSelectPull.GetPull();
            SetButton(obj, unitData);
            _buttons.Add(obj);
        }
    }

    // 버튼의 기능들을 설정
    private void SetButton(ClickUnitSelect unitSelect, UnitDataSO unitData)
    {
        unitSelect.SetButton(unitData);
        unitSelect.OnClick += () => UnitSelector.SelectedUnit(unitData, unitSelect.IsSelectedUnit);
    }
}