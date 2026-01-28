using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommandSO", menuName = "Skill Command SO/Move")]
public class MoveCommandSO : SkillCommandSO
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 4;
    [field: SerializeField] public string AnimationName { get; private set; }
    
    public override ICommand CreateCommand(IActable actable)
    {
        return new MoveCommand(actable, this);
    }
}