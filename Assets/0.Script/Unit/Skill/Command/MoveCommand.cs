using System.Collections;
using UnityEngine;

public class MoveCommand : ICommand
{
    private IMovable _movable;
    private IAnimatable _animatable;
    private string _animationName;
    private float _moveSpeed;
    
    public float Duration { get; private set; }
    
    public MoveCommand(IActable actable, MoveCommandSO so)
    {
        _movable =  actable;
        _animatable = actable;
        _animationName = so.AnimationName;
        _moveSpeed = so.MoveSpeed;
        Duration = so.AfterDelay;
    }


    public IEnumerator Execute(BattleInfo battleInfo)
    {
        float distance = float.MaxValue;
        var target = battleInfo.Target.MyObject;
        _animatable?.PlayAni(_animationName);
        
        // 타겟과의 거리 기준으로 움직임 제어
        while (distance >= 1.5f)
        {
            if (_movable == null)
            {
                Debug.LogWarning("이동 커맨드의 사용객체가 없습니다.");
                yield break;
            }
            
            _movable.Move(target.transform.position, _moveSpeed);
            
            Vector3 myPos = battleInfo.Attacker.MyObject.transform.position;
            distance = Vector3.SqrMagnitude(target.transform.position - myPos);
            yield return null;
        }
    }
}