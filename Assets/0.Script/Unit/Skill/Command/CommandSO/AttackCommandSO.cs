using UnityEngine;

[CreateAssetMenu(fileName = "AttacklCommandSO", menuName = "Skill Command SO/Attack")]
public class AttackCommandSO : SkillCommandSO
{
    [field: SerializeField] public string AnimationName { get; private set; }
    
    public override ICommand CreateCommand(IActable actable)
    {
        return new AttackCommand(actable, this);
    }
}