using System.ComponentModel;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "SkillSO", order = 0)]
public class SkillDataSO : ScriptableObject
{
    [field: SerializeField] public string OwnerName { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int CoinCount { get; private set; }
    [field: SerializeField] public int CoinValue { get; private set; }
    [field: SerializeField] public SkillType Type { get; private set; }
}

public enum SkillType
{
    Skill01, Skill02, Skill03
}