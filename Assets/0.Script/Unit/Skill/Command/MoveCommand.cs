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
        _distance = so.TargetDistance;
        AfterDelay = so.AfterDelay;
    }


    public IEnumerator Execute(BattleInfo battleInfo)
    {
        _animatable?.PlayAni(_animationName);
        float distance = float.MaxValue;
        var target = battleInfo.Target.MyObject;
        
        SetDirection(battleInfo);
        
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
        
        float dirX = target.position.x - attacker.position.x;
        int nomalizedX = (int)( dirX / Mathf.Abs(dirX) );

        switch (battleInfo.AttackerTeam)
        {
            case UnitTeam.Player:
                attacker.localScale = new Vector3(nomalizedX, 1, 1);
                target.localScale = new Vector3(nomalizedX, 1, 1);
                break;
            case UnitTeam.Enemy:
                attacker.localScale = new Vector3(-nomalizedX, 1, 1);
                target.localScale = new Vector3(-nomalizedX, 1, 1);
                break;
        }
    }
}