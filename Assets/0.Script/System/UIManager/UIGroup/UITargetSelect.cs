using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetSelect : UIGroup
{
    private List<Canvas> _buttons;
    private UnityObjectPull<Canvas> _canvasPull;
    
    [SerializeField] private Canvas _targetSelectPrefab;

    
    
    protected override void Awake()
    {
        _buttons = new List<Canvas>();
        _canvasPull = new UnityObjectPull<Canvas>(_targetSelectPrefab, 5, _objectPullTransform);
        base.Awake();
    }

    
    private void OnEnable()
    {
        _buttons.Clear();
        
        OnCreateButton();
    }
    
    private void OnDisable()
    {
        ReleaseButons();
    }

    
    
    // 캔버스에서 ClickObject 찾아서 버튼 생성
    private void OnCreateButton()
    {
        foreach (var enemy in BattleManager.Instance.Enemies)
        {
            if(enemy.IsDead)
                continue;
            
            var canvas = _canvasPull.GetPull(enemy.GetTransform);
            canvas.gameObject.name = enemy.Name;
            
            foreach (Transform child in canvas.transform)
            {
                if (child.TryGetComponent(out ClickObject button))
                    SetButton(button, enemy);
            }
            _buttons.Add(canvas);
        }
    }
    
    // 버튼의 기능 설정
    private void SetButton(ClickObject clickObject, UnitPresenter enemy)
    {
        clickObject.name = enemy.Name + " 선택버튼";
        clickObject.GetComponentInChildren<Text>().text = enemy.Name;
        clickObject.OnClick += () => BattleManager.Instance.SetSequenceTarget(enemy);
    }
    
    private void ReleaseButons()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.name = "TargetSelectButton";
            _canvasPull.Release(button);
        }
    }
    
    public override void ResetPullParnets()
    {
        foreach (var button in _buttons)
        {
            if(!button)
                continue;
            
            button.transform.SetParent(_objectPullTransform);
        }
    }
}