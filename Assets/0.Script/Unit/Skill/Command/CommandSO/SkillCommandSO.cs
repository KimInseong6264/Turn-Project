using UnityEngine;

public abstract class SkillCommandSO : ScriptableObject
{
    [field: SerializeField] public float AfterDelay { get; private set; }
    
    public abstract ICommand CreateCommand(IActable actable);
}