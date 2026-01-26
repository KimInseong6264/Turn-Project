using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "SkillSO", order = 0)]
public class SkillDataSO : ScriptableObject
{
    [field: SerializeField] public string OwnerName { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int CoinCount { get; private set; }
    [field: SerializeField] public int CoinValue { get; private set; }
    [field: SerializeField] public SkillType Type { get; private set; }
    [field: SerializeField] public List<SkillCommandSO> CommandList { get; private set; }

    
    
    public void SetSOValue(SkillDataDTO dto)
    {
        OwnerName = dto.OwnerName;
        Name = dto.Name;
        CoinCount = dto.CoinCount;
        CoinValue = dto.CoinValue;
        Type = dto.Type;
    }
}

public enum SkillType
{
    Skill01, Skill02, Skill03
}