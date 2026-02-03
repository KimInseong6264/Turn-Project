using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSkill : ISkill, ISkillData
{
    private string _ownerName;
    private string _name;
    private SkillType _type;
    private int _coinCount;
    private int _coinValue;
    private Sprite _skillIcon;
    private SkillUI _skillUI;
    private IActable _owner;

    private List<ICommand> _commands;

    public string OwnerName => _ownerName;
    public string Name => _name;
    public SkillType Type => _type;
    public int ConinCount => _coinCount;
    public int CoinValue => _coinValue;
    public Sprite SkillIcon => _skillIcon;

    public static event Action<BattleInfo> OnSkillStart;
    public static event Action OnSkillEnd;
    
    public UnitSkill(SkillDataSO skillData, IActable owner)
    {
        _ownerName = skillData.OwnerName;
        _name = skillData.Name;
        _type = skillData.Type;
        _coinCount = skillData.CoinCount;
        _coinValue = skillData.CoinValue;
        _skillIcon = skillData.SkillIcon;
        
        _skillUI = owner.UnitSkillUI;
        _owner = owner;
        _commands = new List<ICommand>();
        SetCommands(skillData.CommandList, owner);
    }

    public IEnumerator UseSkill(BattleInfo battleInfo)
    {
        OnSkillStart?.Invoke(battleInfo);
        _skillUI.OnSkillUI(battleInfo.Attacker);
        
        foreach (var command in _commands)
        {
            yield return command.Execute(battleInfo);
            
            _owner.PlayAni("Idle");
            yield return CoroutineManager.GetWaitTime(command.AfterDelay);
            
        }
        InitDirection(battleInfo);
        _skillUI.Init();
        OnSkillEnd?.Invoke();
    }

    private void SetCommands(List<SkillCommandSO> commands, IActable owner)
    {
        foreach (var command in commands)
        {
            _commands.Add(command.CreateCommand(owner, this));
        }
    }

    private void InitDirection(BattleInfo battleInfo)
    {
        Transform attacker = battleInfo.Attacker.MyObject.transform;
        Transform target = battleInfo.Target.MyObject.transform;
        
        attacker.localScale = new Vector3(1, 1, 1);
        target.localScale = new Vector3(1, 1, 1);
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