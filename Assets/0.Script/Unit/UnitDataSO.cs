using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitSO", order = 0)]
public class UnitDataSO : ScriptableObject
{

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public UnitTeam Team { get; private set; }
    [field: SerializeField] public int Hp { get; private set; }
    [field: SerializeField] public float AttLevel { get; private set; }
    [field: SerializeField] public float DefLevel { get; private set; }
    [field: SerializeField] public int Speed { get; private set; }
    [field: SerializeField] public List<SkillDataSO> SkillList { get; private set; }
    [field: SerializeField] public UnitView UnitPrefab { get; private set; }
    [field: SerializeField] public Sprite UnitSelectButton { get; private set; }

    
    private void OnValidate()
    {
        SkillSettingCondition();
        UnitSelectButtonComdition();
    }

    
    // 스킬SO를 끼워넣을 때, 스킬Owner와 맞지 않으면 스킬 매칭 불가
    private void SkillSettingCondition()
    {
        if (SkillList == null)
            return;
    
        for(int i = 0; i < SkillList.Count; i++)
        {
            SkillDataSO skill =  SkillList[i];
            if(skill == null)
                continue;
            
            if (skill.OwnerName != Name)
            {
                Debug.LogWarning(skill.Name + "의 소유자가 다릅니다.");
                SkillList[i] = null;
            }
        }
    }

    // 에너미는 유닛선택버튼 설정불가
    private void UnitSelectButtonComdition()
    {
        if (UnitSelectButton != null && Team == UnitTeam.Enemy)
        {
            Debug.LogWarning("Enemy는 UnitSelectButton이 필요하지 않습니다.");
            UnitSelectButton = null;
        }
    }

    public void SetSOValue(UnitDataDTO dto)
    {
        Name = dto.Name;
        Team = dto.Team;
        Hp = dto.Hp;
        AttLevel = dto.AttLevel;
        DefLevel = dto.DefLevel;
        Speed = dto.Speed;
    }
}

public enum UnitTeam
{
    Player, Enemy
}