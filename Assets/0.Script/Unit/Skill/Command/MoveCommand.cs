
using System.Collections;
using UnityEngine;

public class MoveCommand : ICommand
{
    private IMovable _movable;
    private IAnimatable _animatable;
    private string _animationName;
    private float _speed;
    
    public float Duration { get; private set; }
    
    public MoveCommand(ISkillable skillable, MoveCommandSO so)
    {
        _movable =  skillable;
        _animatable = skillable;
        _animationName = so.AnimationName;
        _speed = so.Speed;
        Duration = so.AfterDelay;
    }


    public IEnumerator Execute(IHitable target)
    {
        _animatable?.PlayAni(_animationName);
        Vector3 distance = Vector3.zero;
        
        // 타겟과의 거리 기준으로 움직임 제어
        while (distance == Vector3.zero || distance.sqrMagnitude >= 1.5f)
        {
            if (_movable == null)
            {
                Debug.LogWarning("이동 커맨드의 사용객체가 없습니다.");
                yield break;
            }
            
            
            Vector3 myPos = _movable.Move(target.GetTransform, _speed).position;
            distance = target.GetTransform.position - myPos;
            yield return null;
        }
    }
}