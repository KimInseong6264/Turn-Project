using UnityEngine;

public abstract class SkillCommandSO : ScriptableObject
{
    [field: SerializeField] public float AfterDelay { get; private set; } = 0.5f;
    
    public abstract ICommand CreateCommand(IActable actable, ISkill skill);
}