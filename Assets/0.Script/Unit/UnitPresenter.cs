using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPresenter : IHitable, IUnit
{
    private readonly UnitModel _model;
    private readonly UnitView _view;
    
    // 인터페이스 제공 프로퍼티
    public IUnitData Data { get; private set; }
    public IActable Actable { get; private set; }
    public IHitable Hitable { get; private set; }
    public GameObject MyObject { get; private set; }

    // Model의 정보 제공
    public string Name => _model.Name;
    public int Hp => _model.Hp;
    public bool IsDead => _model.IsDead;
    public int Speed => _model.Speed;
    public UnitTeam Team => _model.Team;
    public UnitSkill Skill => _model.SkillToUse; // 선택된 스킬 확인
    
    // View의 정보 제공
    public UnitView View => _view;
    public Transform MyTransform => _view.transform;


    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        _view = view;
        _model = new UnitModel(unitData, _view);
        
        Data = _model;
        Actable = view;
        Hitable = this;
        MyObject = view.gameObject;
    }
    
    
    public void Tick() {}
    
    
    // Model - View 연동 메서드
    private void OnTakeDamage(int damage)
    {
        _model.TakeDamage(damage);
        _view.UpdateHpBar(_model.Hp, _model.MaxHp);
        if(_model.IsDead)
            _view.SetActiveHpBar(false);
    }
    
    // 피격당할 시 발동
    public void OnHit(BattleInfo battleInfo, int damage, KnockbackInfo? knockbackInfo = null)
    {
        
        OnTakeDamage(damage);
        if (IsDead)
        {
            Debug.Log($"{battleInfo.Target.Data.Name}가 죽었습니다.");
            _view.PlayAni("Death");
            return;
        }

        if (knockbackInfo != null)
            _view.StartCoroutine(Knockback(knockbackInfo));
    }

    // 넉백 관련 메서드
    private IEnumerator Knockback(KnockbackInfo? knockbackInfo)
    {
        var dir =  (knockbackInfo!.Value.Direction + MyTransform.position).normalized;
        Vector3 startPos = MyTransform.position;
        float distance = float.MaxValue;
        while (distance <= knockbackInfo!.Value.Distance)
        {
            _view.Move(dir, knockbackInfo!.Value.Speed);
            distance = Vector3.SqrMagnitude(startPos - MyTransform.position);
            yield return null;
        }
    }
    
    
    // View의 설정 변경
    public void SetPosition(Vector3 pos) => _view.transform.position = pos;
    public void SetObjectName(string name) => _view.gameObject.name = name;

    
    // 스킬 관련 메서드
    public void SetSkill(SkillType skillType) => _model.SetSkillToUse(skillType);
    public void StartSkillExecute(BattleInfo battleInfo) => _view.StartCoroutine(Skill.UseSkill(battleInfo));
}