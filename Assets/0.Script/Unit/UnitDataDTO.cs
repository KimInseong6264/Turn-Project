using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataDTO
{
    public string Name;
    public UnitTeam Team;
    public int Hp;
    public float AttLevel;
    public float DefLevel;
    public int Speed;
    public List<SkillDataDTO> SkillList = new List<SkillDataDTO>();
    public UnitView UnitPrefab;

    public UnitDataDTO(UnitDataSO unitData)
    {
        Name = unitData.Name;
        Team = unitData.Team;
        Hp = unitData.Hp;
        AttLevel = unitData.AttLevel;
        DefLevel = unitData.DefLevel;
        Speed = unitData.Speed;
        UnitPrefab = unitData.UnitPrefab;

        foreach (var skillData in unitData.SkillList)
        {
            SkillList.Add(new SkillDataDTO(skillData));
        }
    }
}

[Serializable]
public class SkillDataDTO
{
    public string OwnerName;
    public string Name;
    public int CoinCount;
    public int CoinValue;
    public SkillType Type;
    public List<CommandDTO> CommandList = new List<CommandDTO>();

    public SkillDataDTO(SkillDataSO skillData)
    {
        OwnerName = skillData.OwnerName;
        Name = skillData.Name;
        CoinCount = skillData.CoinCount;
        CoinValue = skillData.CoinValue;
        Type = skillData.Type;
        
        foreach (var commandData in skillData.CommandList)
        {
            CommandList.Add(new CommandDTO(commandData));
        }
    }
}

[Serializable]
public class CommandDTO
{
    public string CommandType;
    public string Jason;

    public CommandDTO(SkillCommandSO command)
    {
        CommandType = command.GetType().Name;
        Jason = JsonUtility.ToJson(command);
    }
}