using System.Collections;
using UnityEngine;

public class KnockbackAttackCommand : ICommand
{
    private IAttackable _attackable;
    private IAnimatable _animatable;
    private string _animationName;
    private Vector3 _dir;
    
    public float Duration { get; private set; }
    public IEnumerator Execute(IHitable target)
    {
        _animatable?.PlayAni(_animationName);
        _attackable?.Attack();
        yield break;
    }

    public KnockbackAttackCommand(ISkillable skillable, Vector3 KnockbackDir, string animationName, float duration)
    {
        _attackable = skillable;
        _animatable = skillable;
        _animationName = animationName;
        _dir = KnockbackDir;
        Duration = duration;
    }
}