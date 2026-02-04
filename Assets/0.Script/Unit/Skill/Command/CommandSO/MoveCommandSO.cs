using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommandSO", menuName = "Skill Command SO/Move")]
public class MoveCommandSO : SkillCommandSO
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 4;
    [field: SerializeField] public float TargetDistance { get; private set; } = 1.5f;
    [field: SerializeField] public string AnimationName { get; private set; } = "Move";
    
    public override ICommand CreateCommand(IActable actable, ISkill skill)
    {
        return new MoveCommand(actable, skill, this);
    }
}