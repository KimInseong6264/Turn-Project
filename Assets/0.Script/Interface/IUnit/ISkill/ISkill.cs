

using System.Collections;

public interface ISkill
{
    string Name { get; }
    SkillType Type { get; }
    int ConinCount { get; }
    int CoinValue { get; }

    IEnumerator UseSkill(BattleInfo battleInfo);
}