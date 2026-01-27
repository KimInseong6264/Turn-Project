using UnityEngine;


public abstract class SkillCommandSO : ScriptableObject
{
    [field: SerializeField] public float AfterDelay {get; private set;} 
    
    public abstract ICommand CreateCommand(IActable actable);
}

[CreateAssetMenu(fileName = "SkillCommandSO", menuName = "Skill Command SO/Move")]
public class MoveCommandSO : SkillCommandSO
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public string AnimationName { get; private set; }
    
    public override ICommand CreateCommand(IActable actable)
    {
        return new MoveCommand(actable, this);
    }
}

[CreateAssetMenu(fileName = "SkillCommandSO", menuName = "Skill Command SO/Attack")]
public class AttackCommandSO : SkillCommandSO
{
    [field: SerializeField] public string AnimationName { get; private set; }
    
    public override ICommand CreateCommand(IActable actable)
    {
        return new AttackCommand(actable, this);
    }
}
