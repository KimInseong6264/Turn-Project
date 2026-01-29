using System.Collections;
using System.Collections.Generic;

public class UnitSkill : ISkill
{
    private string _ownerName;
    private string _name;
    private SkillType _type;
    private int _coinCount;
    private int _coinValue;
    private SkillUI _skillUI;
    private IActable _owner;

    private List<ICommand> _commands;

    public string OwnerName => _ownerName;
    public string Name => _name;
    public SkillType Type => _type;
    public int ConinCount => _coinCount;
    public int CoinValue => _coinValue;
    
    public UnitSkill(SkillDataSO skillData, IActable owner)
    {
        _ownerName = skillData.OwnerName;
        _name = skillData.Name;
        _type = skillData.Type;
        _coinCount = skillData.CoinCount;
        _coinValue = skillData.CoinValue;
        _skillUI = owner.UnitSkillUI;
        _owner =  owner;
        _commands = new List<ICommand>();
        SetCommands(skillData.CommandList, owner);
    }

    public IEnumerator UseSkill(BattleInfo battleInfo)
    {
        _skillUI.OnSkillUI(battleInfo.Attacker);
        
        foreach (var command in _commands)
        {
            yield return command.Execute(battleInfo);
            
            _owner.PlayAni("Idle");
            yield return CoroutineManager.GetWaitTime(command.AfterDelay);
        }
        
        _skillUI.Init();
    }

    private void SetCommands(List<SkillCommandSO> commands, IActable owner)
    {
        foreach (var command in commands)
        {
            _commands.Add(command.CreateCommand(owner, this));
        }
    }
}

/// <summary>
/// 스킬 생성 팩토리
/// </summary>
public static class SkillFactory
{
    public static UnitSkill CreateSkill(SkillDataSO skillDataSo, IActable owner)
    {
        return new UnitSkill(skillDataSo, owner);
    }
}