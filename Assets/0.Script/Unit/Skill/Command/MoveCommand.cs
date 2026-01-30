using System.Collections;
using UnityEngine;

public class MoveCommand : ICommand
{
    private IMovable _movable;
    private IAnimatable _animatable;
    private string _animationName;
    private float _moveSpeed;
    private float _distance;
    
    public float AfterDelay { get; private set; }
    
    public MoveCommand(IActable actable, ISkill skill, MoveCommandSO so)
    {
        _movable =  actable;
        _animatable = actable;
        _animationName = so.AnimationName;
        _moveSpeed = so.MoveSpeed;
        _distance = so.Distance;
        AfterDelay = so.AfterDelay;
    }


    public IEnumerator Execute(BattleInfo battleInfo)
    {
        float distance = float.MaxValue;
        var target = battleInfo.Target.MyObject;
        
        SetDirection(battleInfo);
        _animatable?.PlayAni(_animationName);
        
        // 타겟과의 거리 기준으로 움직임 제어
        while (distance >= _distance)
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
    
    private void SetDirection(BattleInfo battleInfo)
    {
        Transform attacker = battleInfo.Attacker.MyObject.transform;
        Transform target = battleInfo.Target.MyObject.transform;
        
        float dir = target.position.x - attacker.position.x;

        if (dir < 0)
        {
            attacker.localScale = new Vector3(-1, 1, 1);
            target.localScale = new Vector3(-1, 1, 1);
        }
    }
}