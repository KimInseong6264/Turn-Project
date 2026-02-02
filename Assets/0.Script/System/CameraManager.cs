using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineCamera _currentCinemachineCamera;
    private CinemachineTargetGroup _targetGroup;
    [SerializeField] private CinemachineCamera _mainCinemachine;
    [SerializeField] private CinemachineCamera _targetCinemachine;

    public static event Action OnSetTargetEnd;

    private void Awake()
    {
        _targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        _currentCinemachineCamera = _mainCinemachine;
        _currentCinemachineCamera.Priority.Value = 20;
    }

    private void OnEnable()
    {
        UnitSkill.OnSkillStart += SetTarget;
        UnitSkill.OnSkillEnd += SetCurrentMainCinemachine;
    }
    
    private void OnDisable()
    {
        UnitSkill.OnSkillStart -= SetTarget;
        UnitSkill.OnSkillEnd -= SetCurrentMainCinemachine;
    }

    
    public void ChangeCinemachine(string name)
    {
        switch (name)
        {
            case "Main":
                SetCurrentCinemachine(_mainCinemachine);
                break;
            case "Target":
                SetCurrentCinemachine(_targetCinemachine);
                break;
        }
    }
    
    // 카메라 전환시 메커니즘
    private void SetCurrentCinemachine(CinemachineCamera cinemachineCamera)
    {
        _currentCinemachineCamera.Priority.Value = 10;
        cinemachineCamera.Priority.Value = 20;
        _currentCinemachineCamera = cinemachineCamera;
    }

    // TargetGroup에 타겟 세팅
    private void SetTarget(BattleInfo battleInfo)
    {
        Debug.Log("설마 널이니?" + _targetGroup);
        if (_targetGroup == null) 
            return;
     
        _targetGroup.Targets.Clear();
        AddTargetToGroup(battleInfo.Attacker);
        AddTargetToGroup(battleInfo.Target);

        foreach (var a in _targetGroup.Targets)
        {
            Debug.Log(a.Object.name);
        }
        
        OnSetTargetEnd?.Invoke();
    }
    
    // TargetGroup에 넣기 위해 Unit -> 타겟으로 변형
    private void AddTargetToGroup(IUnit unit, float weight = 1f, float radius = 2f)
    {
        CinemachineTargetGroup.Target cinemachineTarget = new CinemachineTargetGroup.Target
        {
            Object = unit.MyObject.transform,
            Weight = weight,
            Radius = radius
        };
        _targetGroup.Targets.Add(cinemachineTarget);
    }
    
    private void SetCurrentMainCinemachine() => SetCurrentCinemachine(_mainCinemachine);
}
