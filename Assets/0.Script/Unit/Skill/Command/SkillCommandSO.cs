using UnityEngine;


public abstract class SkillCommandSO : ScriptableObject
{
    [field: SerializeField] public float AfterDelay {get; private set;} 
    
    public abstract ICommand CreateCommand(ISkillable skillable);
}

[CreateAssetMenu(fileName = "SkillCommandSO", menuName = "Skill Command SO/Move")]
public class MoveCommandSO : SkillCommandSO
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public string AnimationName { get; private set; }
    
    public override ICommand CreateCommand(ISkillable skillable)
    {
        return new MoveCommand(skillable, this);
    }
}

[CreateAssetMenu(fileName = "SkillCommandSO", menuName = "Skill Command SO/Attack")]
public class AttackCommandSO : SkillCommandSO
{
    [field: SerializeField] public string AnimationName { get; private set; }
    
    public override ICommand CreateCommand(ISkillable skillable)
    {
        return new AttackCommand(skillable, this);
    }
}
