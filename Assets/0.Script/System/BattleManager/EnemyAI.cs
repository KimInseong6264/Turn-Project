using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public BattleInfo SetEnemyInfo(BattleInfo enemyInfo)
    {
        BattleInfo battleInfo =
            new BattleInfo(
                enemyInfo,
                SetEnemyInfoSkill(enemyInfo.Attacker),
                SetEnemyInfoTarget(enemyInfo.Attacker)
                );
        
        return battleInfo;
    }

    // 에너미의 사용할 스킬을 랜덤 선택
    private UnitSkill SetEnemyInfoSkill(UnitPresenter enemy)
    {
        Dictionary<SkillType, UnitSkill> skills = enemy.GetSkills();
        int count = skills.Count;
        int random = Random.Range(0, count);
        
        return skills[(SkillType)random];
    }

    // 에너미가 공격할 타겟 랜덤 선택
    private UnitPresenter SetEnemyInfoTarget(UnitPresenter enemy)
    {
        List<UnitPresenter> players = BattleManager.Instance.Players;

        int playerCount = 0;
        foreach (UnitPresenter player in players)
        {
            if (!player.IsDead)
                break;
            playerCount++;
        }

        if (playerCount == players.Count)
            return null;
        
        int random = Random.Range(0, players.Count);
        while(players[random].IsDead)
        {
            random = Random.Range(0, players.Count);
        }
        
        return players[random];
    }
}