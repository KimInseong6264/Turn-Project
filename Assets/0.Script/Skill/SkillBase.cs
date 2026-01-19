using System;
using System.Collections;
using UnityEngine;

public abstract class SkillBase
{
    protected string _ownerName;
    protected string _name;
    protected SkillType _type;
    protected int _coinCount;
    protected int _coinValue;
    protected ISkillable _unit;

    public string OwnerName => _ownerName;
    public string Name => _name;
    public SkillType Type => _type;
    
    protected SkillBase(SkillDataSO skillData, ISkillable unit)
    {
        _ownerName = skillData.OwnerName;
        _name = skillData.Name;
        _type = skillData.Type;
        _coinCount = skillData.CoinCount;
        _coinValue = skillData.CoinValue;
        _unit = unit;
    }

    public virtual IEnumerator Execute(UnitPresenter target)
    {
        target.OnTakeDamage(_coinCount + _coinValue);
        Debug.Log("================================");
        Debug.Log("<color=red>스킬이름</color>" + _name);
        Debug.Log("<color=red>스킬코인수</color>" + _coinCount);
        Debug.Log("<color=red>스킬코인벨류</color>" + _coinValue);
        Debug.Log("<color=red>스킬코인벨류</color>" + _coinValue);
        Debug.LogWarning("<color=red>남은 체력</color>" + target.Hp);
        Debug.Log("================================");
        yield break;
    }
}

/// <summary>
/// 스킬 생성 팩토리
/// </summary>
public static class SkillFactory
{
    public static SkillBase CreateSkill(SkillDataSO skillDataSo, ISkillable unit)
    {
        switch (skillDataSo.OwnerName)
        {
            case "플레이어" or "플레이어1" or "플레이어2":
                switch (skillDataSo.Type)
                {
                    case SkillType.Skill01:
                        return new Player.Skill01(skillDataSo, unit);
                    case SkillType.Skill02:
                        return new Player.Skill02(skillDataSo, unit);
                }
                break;
            case "에너미" or "에너미1" or "에너미2":
                switch (skillDataSo.Type)
                {
                    case SkillType.Skill01:
                        return new Enemy.Skill01(skillDataSo, unit);
                    case SkillType.Skill02:
                        return new Enemy.Skill02(skillDataSo, unit);
                }
                break;
            default:
                Debug.LogError(skillDataSo.OwnerName + "은 스킬을 끼울 수 없습니다.");
                break;
        }
        return null;
    }
}