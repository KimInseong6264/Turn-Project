using UnityEngine;

public interface IUnit
{
    IUnitData Data { get; }
    IActable Actable { get; }
    IHitable Hitable { get; }
    GameObject MyObject { get; }

    void SetSkill(SkillType skillType);
    void StartSkillExecute(BattleInfo battleInfo);
}


// 배틀정보 저장 구조체
public struct BattleInfo
{
    public int Speed { get; }
    public UnitTeam Team { get; }
    public UnitSkill SelectedSkill { get; }
    public IUnit Attacker { get; }
    public IUnit Target { get; }

    
    //
    public BattleInfo(IUnit attacker, IUnit target, int speed, UnitTeam team)
    {
        Speed = speed;
        Team =  team;
        Attacker = attacker;
        SelectedSkill = null;
        Target = target;
    }
    //
    
    
    public BattleInfo(UnitPresenter attacker)
    {
        Speed = attacker.Speed;
        Team =  attacker.Team;
        Attacker = attacker;
        SelectedSkill = null;
        Target = null;
        BattleManager.Instance.OnInfoStart();
    }

    // 스킬만 갱신하는 생성자
    public BattleInfo(BattleInfo mySelf, UnitSkill skill, UnitPresenter target = null)
    {
        this.Speed = mySelf.Speed;
        this.Team =  mySelf.Team;
        this.Attacker = mySelf.Attacker;
        this.SelectedSkill = skill;
        this.Target = target;
        BattleManager.Instance.OnInfoStart();
    }

    public void OnBattleExcute()
    {
        Attacker.SetSkill(SelectedSkill.Type);
        Attacker.StartSkillExecute(this);
    }
}