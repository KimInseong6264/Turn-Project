using UnityEngine;

public interface ISkillData
{
    string OwnerName { get; }
    string Name { get; }
    SkillType Type { get; }
    int ConinCount { get; }
    int CoinValue { get; }
    Sprite SkillIcon { get; }
}