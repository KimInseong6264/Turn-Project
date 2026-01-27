using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkill
{
    private string _ownerName;
    private string _name;
    private SkillType _type;
    private int _coinCount;
    private int _coinValue;
    private IActable _owner;

    protected List<ICommand> _commands;

    public string OwnerName => _ownerName;
    public string Name => _name;
    public SkillType Type => _type;
    
    public UnitSkill(SkillDataSO skillData, IActable owner)
    {
        _ownerName = skillData.OwnerName;
        _name = skillData.Name;
        _type = skillData.Type;
        _coinCount = skillData.CoinCount;
        _coinValue = skillData.CoinValue;
        _owner =  owner;
        _commands = new List<ICommand>();
        SetCommands(skillData.CommandList, owner);
    }

    public virtual IEnumerator UseSkill(BattleInfo battleInfo)
    {
        foreach (var command in _commands)
        {
            yield return command.Execute(battleInfo);
            
            _owner.PlayAni("Idle");
            yield return CoroutineManager.GetWaitTime(command.Duration);
        }
        _owner.PlayAni("Idle");
    }

    private void SetCommands(List<SkillCommandSO> commands, IActable owner)
    {
        foreach (var command in commands)
        {
            _commands.Add(command.CreateCommand(owner));
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