using System.Collections;

public class BattleInfoDraft
{
    public int Speed { get; private set; }
    public UnitTeam AttackerTeam { get; private set; }
    public ISkill SelectedSkill { get; private set; }
    public IUnit Attacker { get; private set; }
    public IUnit Target { get; private set; }
    
    public BattleInfoDraft(UnitPresenter attacker)
    {
        Speed = attacker.Speed;
        AttackerTeam =  attacker.Team;
        Attacker = attacker;
        SelectedSkill = null;
        Target = null;
    }
    
    public void SelectSkill(ISkill skill) => SelectedSkill = skill;
    public void SelectTarget(IUnit target) => Target = target;
    
    public IEnumerator OnBattleExcute()
    {
        Attacker.SetSkill(SelectedSkill.Type);
        
        // yield return Attacker.StartSkillExecute(this);
        
        yield break;
    }
}
