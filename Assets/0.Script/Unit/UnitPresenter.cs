using System.Collections.Generic;
using UnityEngine;

public class UnitPresenter : ISkillable
{
    private UnitModel _model;

    private UnitView _view;
    
    public string Name => _model.Name;
    public int Speed => _model.Speed;
    public UnitTeam Team => _model.Team;
    public SkillBase Skill => _model.SkillToUse; // 선택된 스킬 확인
    
    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        _view = view;
        _model = new UnitModel(unitData, this);
    }
    
    public void Tick() {}
    
    public void OnAct() {}
    public void OnMove(Vector3 dir) => _view.transform.Translate(dir);
    
    public void SetPosition(Vector3 pos) => _view.transform.position = pos;
    public void SetObjectName(string name) => _view.gameObject.name = name;

    // 스킬 메서드
    public void SetSkill(SkillType skillType) => _model.SetSkillToUse(skillType);
    public void StartSkillExecute() => _view.StartCoroutine(Skill.Execute());
    public Dictionary<SkillType, SkillBase> GetSkills() => _model.Skills;
}