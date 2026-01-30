using System;
using System.Collections.Generic;
using UnityEngine;


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
        if(_targetSelector == null)
            _targetSelector = BattleManager.Instance;
        _targetSelector.OnTargetSelected += OnCreateArrow;
        _targetSelectUI.OnCreatedTargetButton += AddCreateButon;
    }

    private void OnDisable()
    {
        _targetSelector.OnTargetSelected -= OnCreateArrow;
        _targetSelectUI.OnCreatedTargetButton -= AddCreateButon;
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
        Transform targetTr = target.MyObject.transform;
    
        Vector2 dir = targetTr.position;
        arrowRect.up = dir;
        dir /= arrowRect.localScale.x;
        arrowRect.sizeDelta = new Vector2(arrowRect.sizeDelta.x, dir.magnitude);
    }

    // 화살표 생성
    private RectTransform CreateArrow(IUnit attacker, Transform buttonTransform)
    {
        RectTransform myRect = buttonTransform as RectTransform;
        if(!myRect)
            return null;

        var arrowRect = Instantiate(_arrowPrefab, buttonTransform).GetComponent<RectTransform>();
        arrowRect.anchoredPosition = Vector2.zero;
        
        return arrowRect;
    }

    private void Init()
    {
        _arrowRects.Clear();
        _createdButons.Clear();
    }
}