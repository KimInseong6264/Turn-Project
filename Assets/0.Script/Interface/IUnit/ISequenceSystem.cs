using System.Collections;
using System.Collections.Generic;

public interface ISequenceSystem
{
    Dictionary<string, BattleInfo> BattleSequence { get; }

    void SetSequenceSkill(UnitSkill skill);

    void SetSequenceTarget(UnitPresenter target);
}

// 배틀정보 저장 구조체
public readonly struct BattleInfo
{
    public int Speed { get; }
    public UnitTeam AttackerTeam { get; }
    public ISkill SelectedSkill { get; }
    public IUnit Attacker { get; }
    public IUnit Target { get; }
    
    public BattleInfo(UnitPresenter attacker)
    {
        Speed = attacker.Speed;
        AttackerTeam =  attacker.Team;
        Attacker = attacker;
        SelectedSkill = null;
        Target = null;
    }

    // 스킬만 갱신하는 생성자
    public BattleInfo(BattleInfo mySelf, ISkill skill, UnitPresenter target = null)
    {
        this.Speed = mySelf.Speed;
        this.AttackerTeam =  mySelf.AttackerTeam;
        this.Attacker = mySelf.Attacker;
        this.SelectedSkill = skill;
        this.Target = target;
    }

    public IEnumerator OnBattleExcute()
    {
        Attacker.SetSkill(SelectedSkill.Type);
        
        yield return Attacker.StartSkillExecute(this);
    }
}