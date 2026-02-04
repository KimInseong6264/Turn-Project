using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommandSO", menuName = "Skill Command SO/Stop")]
public class StopCommandSO : SkillCommandSO
{
    [field: SerializeField] public float Duration { get; private set; } = 2f;
    [field: SerializeField] public string AnimationName { get; private set; } = "Idle";
    
    public override ICommand CreateCommand(IActable actable, ISkill skill)
    {
        return new StopCommand(actable, skill, this);
    }
}