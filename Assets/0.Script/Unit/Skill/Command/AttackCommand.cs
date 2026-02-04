using System.Collections;
using UnityEngine;

public class AttackCommand : ICommand
{
    private IAttackable _attackable;
    private IAnimatable _animatable;
    private ISkill _skill;
    private CoinSystem _coinSystem;
    private UnitAttackEvent _attackEvent;
    private string _animationName;
    
    public float AfterDelay { get; private set; }

    public AttackCommand(IActable actable, ISkill skill, AttackCommandSO so)
    {
        _attackable = actable;
        _animatable = actable;
        _skill = skill;
        _coinSystem = actable.UnitSkillUI.CoinSystemHandle;
        _animationName = so.AnimationName;
        _attackEvent = actable.MyObject.GetComponent<UnitAttackEvent>();
        AfterDelay = so.AfterDelay;
    }

    public IEnumerator Execute(BattleInfo battleInfo)
    {
        yield return _coinSystem.OnCoinToss(_skill.ConinCount, _skill.CoinValue, 0.75f, 0.3f);
        
        int finalCoinValue = _coinSystem.CoinValueSum;
        // 타격이 되는 애니메이션에서 타격 메서드 등록
        _attackEvent.ClearOnAttackHitStarted();
        _attackEvent.OnAttackHitStarted += () => _attackable.Attack(battleInfo, finalCoinValue);
        
        _animatable.PlayAni(_animationName);
        yield return null;

        float aniLength = _animatable.MyAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return CoroutineManager.GetWaitTime(aniLength);
    }
    
}