using System.Collections;
using UnityEngine;

public class AttackCommand : ICommand
{
    private IAttackable _attackable;
    private IAnimatable _animatable;
    private UnitAttackEvent _attackEvent;
    private string _animationName;

    public float Duration { get; private set; }
    
    public AttackCommand(IActable actable, AttackCommandSO so)
    {
        _attackable = actable;
        _animatable = actable;
        _animationName = so.AnimationName;
        _attackEvent = actable.MyObject.GetComponent<UnitAttackEvent>();
        Duration = so.AfterDelay;
    }

    public IEnumerator Execute(BattleInfo battleInfo)
    {
        // 타격이 되는 애니메이션에서 타격 메서드 등록
        _attackEvent.ClearOnAttackHiStarted();
        _attackEvent.OnAttackHitStarted += () => _attackable.Attack(battleInfo);
        
        _animatable.PlayAni(_animationName);
        yield return null;

        float aniLength = _animatable.MyAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return CoroutineManager.GetWaitTime(aniLength);
    }
    
}