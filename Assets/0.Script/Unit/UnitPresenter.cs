using System.Collections;
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
    public int Speed => Random.Range(_model.MinSpeed, _model.MaxSpeed + 1);
    public UnitTeam Team => _model.Team;
    public ISkill Skill => _model.SkillToUse; // 선택된 스킬 확인
    
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
        
        _view.gameObject.name = unitData.Name;
    }

    public void Initailize()
    {
        _view.UpdateHpText(_model.Hp);
        _view.UpdateHpBar(_model.Hp, _model.MaxHp);
    }
    
    
    public void Tick() {}
    
    
    // Model - View 연동 메서드
    private void OnTakeDamage(int damage)
    {
        _model.TakeDamage(damage);
        _view.UpdateHpBar(_model.Hp, _model.MaxHp);
        _view.UpdateHpText(_model.Hp);
        if(_model.IsDead)
            _view.SetActiveHpBar(false);
    }
    
    // 피격당할 시 발동
    public void OnHit(BattleInfo battleInfo, int finalCoinValue, KnockbackInfo? knockbackInfo = null)
    {
        int damage = DamageCalculator.CalculateDamage(battleInfo, finalCoinValue);
        OnTakeDamage(damage);
        _view.StartCoroutine(OnHurt());
    }

    private IEnumerator OnHurt()
    {
        _view.PlayAni("Hurt");
        yield return null;

        if (IsDead)
        {
            _view.PlayAni("Death");
            yield break;
        }
        
        var aniInfo = _view.MyAnimator.GetCurrentAnimatorStateInfo(0);
        yield return CoroutineManager.GetWaitTime(aniInfo.length);
        // _view.PlayAni("Idle");
    }
    
    
    // View의 설정 변경
    public void SetPosition(Vector3 pos) => _view.transform.position = pos;

    
    // 스킬 관련 메서드
    public void SetSkill(SkillType skillType) => _model.SetSkillToUse(skillType);
    public IEnumerator StartSkillExecute(BattleInfo battleInfo) => Skill.UseSkill(battleInfo);
}