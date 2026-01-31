using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UITargetSelect))]
public class UITargetSelectCompleted : MonoBehaviour
{
    private Dictionary<IUnit, ClickObject> _createdButons;    // 키: 타겟, 벨류: 타겟의 선택 버튼
    private Dictionary<IUnit, RectTransform> _arrowRects;    // 키: 어태커 , 벨류: 화살표UI오브젝트
    private UITargetSelect _targetSelectUI;
    private ITargetSelect _targetSelector;
    
    [SerializeField] private GameObject _arrowPrefab;

    private RectTransform _spawnTransform;
    // private List<RaycastResult> _raycastResults;

    private void Awake()
    {
        _createdButons = new Dictionary<IUnit, ClickObject>();
        _arrowRects = new Dictionary<IUnit, RectTransform>();
        _targetSelectUI = GetComponent<UITargetSelect>();
    }

    private void OnEnable()
    {
        if(!BattleManager.Instance)
            return;
        
        if(_targetSelector == null)
            _targetSelector = BattleManager.Instance;
        _targetSelectUI.OnCreatedTargetButton += AddCreateButon;
        _targetSelector.OnTargetSelected += OnCreateArrow;
        _targetSelector.OnResetTargetSelected += DestroyArrow;
    }

    private void OnDisable()
    {
        if(!BattleManager.Instance)
            return;
        
        _targetSelectUI.OnCreatedTargetButton -= AddCreateButon;
        _targetSelector.OnTargetSelected -= OnCreateArrow;
        _targetSelector.OnResetTargetSelected -= DestroyArrow;
        Init();
    }

    private void AddCreateButon(IUnit enemy, ClickObject clickObject) => _createdButons.Add(enemy, clickObject);

    private void OnCreateArrow(BattleInfo battleInfo)
    {
        var arrow = CreateArrow(battleInfo.Attacker, battleInfo.Attacker.MyObject.transform);
        SetArrow(battleInfo.Target, arrow);
        _arrowRects.Add(battleInfo.Attacker, arrow);
    }
    
    private void SetArrow(IUnit target, RectTransform arrowRect)
    {
        var targetButton = _createdButons[target];
        Transform targetTr = targetButton.transform;
    
        Vector2 dir = targetTr.position - arrowRect.position;
        
        arrowRect.up = dir;
        
        float distance = dir.magnitude /  arrowRect.lossyScale.y;
        arrowRect.sizeDelta = new Vector2(arrowRect.sizeDelta.x, distance);
    }

    // 화살표 생성
    private RectTransform CreateArrow(IUnit attacker, Transform buttonTransform)
    {
        var arrowRect = Instantiate(_arrowPrefab, buttonTransform.Find("UnitCanvas")).GetComponent<RectTransform>();
        arrowRect.anchoredPosition = Vector2.zero;
        
        return arrowRect;
    }

    private void DestroyArrow(BattleInfo battleInfo)
    {
        if(!_arrowRects.TryGetValue(battleInfo.Attacker, out var arrowRect))
            return;
        
        _arrowRects.Remove(battleInfo.Attacker);
        Destroy(arrowRect.gameObject);
    }

    private void Init()
    {
        foreach (var arrowRect in _arrowRects.Values)
        {
            Destroy(arrowRect.gameObject);
        }
        _arrowRects.Clear();
        _createdButons.Clear();
    }
}