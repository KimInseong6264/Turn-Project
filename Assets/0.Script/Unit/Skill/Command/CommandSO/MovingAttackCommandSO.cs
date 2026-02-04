using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommandSO", menuName = "Skill Command SO/MovingAttack")]
public class MovingAttackCommandSO : SkillCommandSO
{
    [field: SerializeField] public Vector3 MoveDirection { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 6f;
    [field: SerializeField] public float MoveDistance { get; private set; } = 4f;
    [field: SerializeField] public string AnimationName { get; private set; } = "Move";
    
    public override ICommand CreateCommand(IActable actable, ISkill skill)
    {
        return new MovingAttackCommand(actable, skill, this);
    }
}