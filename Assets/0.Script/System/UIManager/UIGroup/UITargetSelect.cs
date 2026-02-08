using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetSelect : UIGroup
{
    private List<Transform> _buttons;
    private ObjectPull<Transform> _buttonPull;
    private ISequenceSystem _sequenceSystem;
    
    public event Action<UnitPresenter, ClickObject> OnCreatedTargetButton;
    
    [SerializeField] private RectTransform _targetSelectPrefab;
    [SerializeField] private Transform _buttonParent;

    
    
    protected override void Awake()
    {
        _buttons = new List<Transform>();
        _buttonPull = new ObjectPull<Transform>(_targetSelectPrefab, 5, _buttonParent);
        base.Awake();
    }

    private void Start()
    {
        _sequenceSystem = BattleManager.Instance;
    }


    private void OnEnable()
    {
        _buttons.Clear();
        
        StartCoroutine(OnCreateButton());
    }
    
    private void OnDisable()
    {
        ReleaseButons();
    }

    
    
    // 캔버스에서 ClickObject 찾아서 버튼 생성
    private IEnumerator OnCreateButton()
    {
        yield return null;
        CreateButton(BattleManager.Instance.Players);
        CreateButton(BattleManager.Instance.Enemies);
    }

    private void CreateButton(List<UnitPresenter> units)
    {
        foreach (var unit in units)
        {
            if(unit.IsDead)
                continue;
            
            Transform unitCanvas = unit.MyTransform.GetComponentInChildren<Canvas>().transform;
            var targetSelector = _buttonPull.GetPull(unitCanvas);
            targetSelector.gameObject.name = "TargetSelectButton";
            
            foreach (Transform child in targetSelector.transform)
            {
                if (child.TryGetComponent(out ClickObject button))
                    SetButton(button, unit);
            }
            _buttons.Add(targetSelector);
        }
    }
    
    // 버튼의 기능 설정
    private void SetButton(ClickObject clickObject, UnitPresenter unit)
    {
        if (unit.Team == UnitTeam.Player)
        {
            clickObject.GetComponent<Button>().interactable = false;
        }
        else if(unit.Team == UnitTeam.Enemy)
        {
            clickObject.GetComponent<Button>().interactable = true;
            clickObject.OnClick += () => _sequenceSystem.SetSequenceTarget(unit);
        }
        clickObject.name = unit.Name + " 선택버튼";
        OnCreatedTargetButton?.Invoke(unit, clickObject);
    }
    
    private void ReleaseButons()
    {
        foreach (var transform1 in _buttons)
        {
            var button = (RectTransform)transform1;
            button.gameObject.name = "TargetSelectButton";
            
            button.localScale = _targetSelectPrefab.localScale;
            
            _buttonPull.Release(button);
        }
    }
    
    public override void ResetPullParnets()
    {
        foreach (var button in _buttons)
        {
            if(!button)
                continue;
            
            button.transform.SetParent(_buttonParent);
        }
    }
}