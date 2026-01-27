using System.Collections;
using UnityEngine;


// 넉백 정보 인가도 만들어야 한다.


public class KnockbackAttackCommand : ICommand
{
    private IAttackable _attackable;
    private IAnimatable _animatable;
    private string _animationName;
    private Vector3 _dir;
    
    public float Duration { get; private set; }
    public IEnumerator Execute(BattleInfo battleInfo)
    {
        // _animatable?.PlayAni(_animationName);
        // _attackable?.Attack();
        yield break;
    }

    public KnockbackAttackCommand(IActable actable, Vector3 KnockbackDir, string animationName, float duration)
    {
        _attackable = actable;
        _animatable = actable;
        _animationName = animationName;
        _dir = KnockbackDir;
        Duration = duration;
    }
}

public struct KnockbackInfo
{
    public readonly Vector3 Direction;
    public readonly float Distance;
    public readonly float Speed;
    
    public KnockbackInfo(Vector3 direction, float distance, float speed)
    {
        Direction = direction;
        Distance = distance * distance;
        Speed = speed;
    }
}