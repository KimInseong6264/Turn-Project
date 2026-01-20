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
    [field:SerializeField] public UnitView UnitPrefab { get; private set; }

    
    private void OnValidate()
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
}

public enum UnitTeam
{
    Player, Enemy
}