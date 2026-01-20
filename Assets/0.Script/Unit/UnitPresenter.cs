using System.Collections.Generic;
using UnityEngine;

public class UnitPresenter : ISkillable
{
    private UnitModel _model;
    private UnitView _view;
    
    
    // Model의 정보 제공
    public string Name => _model.Name;
    public int Hp => _model.Hp;
    public bool IsDead => _model.IsDead;
    public int Speed => _model.Speed;
    public UnitTeam Team => _model.Team;
    public SkillBase Skill => _model.SkillToUse; // 선택된 스킬 확인
    
    // View의 정보 제공
    public Transform GetTransform => _view.transform;
    
    
    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        _view = view;
        _model = new UnitModel(unitData, this);
    }
    
    
    public void Tick() {}
    
    
    // Model - View 연동 메서드
    public void OnTakeDamage(int damage)
    {
        _model.TakeDamage(damage);
        _view.UpdateHpBar(_model.Hp, _model.MaxHp);
        if(_model.IsDead)
            _view.SetActiveHpBar(false);
    }
    public void OnAct() {}
    public void OnMove(Vector3 dir) => _view.transform.Translate(dir);
    
    
    // View의 설정 변경
    public void SetPosition(Vector3 pos) => _view.transform.position = pos;
    public void SetObjectName(string name) => _view.gameObject.name = name;

    
    // 스킬 관련 메서드
    public void SetSkill(SkillType skillType) => _model.SetSkillToUse(skillType);
    public void StartSkillExecute(UnitPresenter target) => _view.StartCoroutine(Skill.Execute(target));
    public Dictionary<SkillType, SkillBase> GetSkills() => _model.Skills;
}