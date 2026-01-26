using System.Collections;
using UnityEngine;

public class AttackCommand : ICommand
{
    private IAttackable _attackable;
    private IAnimatable _animatable;
    private string _animationName;

    public float Duration { get; private set; }
    
    public AttackCommand(ISkillable skillable, AttackCommandSO so)
    {
        _attackable = skillable;
        _animatable = skillable;
        _animationName = so.AnimationName;
        Duration = so.AfterDelay;
    }

    public IEnumerator Execute(IHitable target)
    {
        _animatable?.PlayAni(_animationName);
        _attackable?.Attack();
        yield return null;

        float aniLength = _animatable.GetAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return CoroutineManager.GetWaitTime(aniLength);
    }
}