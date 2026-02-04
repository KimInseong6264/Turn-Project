using System.Collections;
using UnityEngine;

public class MovingAttackCommand : ICommand
{
    private IMovable _movable;
    private IAnimatable _animatable;
    private ISkill _skill;
    private Vector3 _startPos;
    private Vector3 _dir;
    private string _animationName;
    private float _moveSpeed;
    private float _moveDistance;
    private CoinSystem _coinSystem;
    private UnitAttackEvent _attackEvent;
    
    public float AfterDelay { get; private set; }
    
    public MovingAttackCommand(IActable actable, ISkill skill, MovingAttackCommandSO so)
    {
        _movable =  actable;
        _animatable = actable;
        _skill = skill;
        _dir = so.MoveDirection.normalized;
        _animationName = so.AnimationName;
        _moveSpeed = so.MoveSpeed;
        _moveDistance = so.MoveDistance * so.MoveDistance;
        _coinSystem = actable.UnitSkillUI.CoinSystemHandle;
        _attackEvent = actable.MyObject.GetComponent<UnitAttackEvent>();
        AfterDelay = so.AfterDelay;
    }


    public IEnumerator Execute(BattleInfo battleInfo)
    {
        Transform attacker = battleInfo.Attacker.MyObject.transform;
        Transform target = battleInfo.Target.MyObject.transform;
        if (_dir == Vector3.zero)
            _dir = (target.position - attacker.position).normalized;
        _startPos = attacker.position;
        
        yield return _coinSystem.OnCoinToss(_skill.ConinCount, _skill.CoinValue, 0.75f, 0.3f);
        
        int finalCoinValue = _coinSystem.CoinValueSum;
        // 타격이 되는 애니메이션에서 타격 메서드 등록
        _attackEvent.ClearOnAttackHitStarted();
        _attackEvent.OnAttackHitStarted += () => battleInfo.Attacker.Actable.Attack(battleInfo, finalCoinValue);
        
        _animatable?.PlayAni(_animationName);
        SetUnitDirection(battleInfo.Attacker, _dir);
        
        // 타겟과의 거리 기준으로 움직임 제어
        while (Vector3.SqrMagnitude(attacker.position - _startPos) <= _moveDistance)
        {
            _movable.Move(attacker.position + _dir, _moveSpeed);
            yield return null;
        }
    }
    
    private void SetUnitDirection(IUnit unit, Vector3 dir)
    {
        Transform attacker = unit.MyObject.transform;

        float dirX = dir.x;
        int normalizedX = (int)( dirX / Mathf.Abs(dirX) );

        switch (unit.Data.Team)
        {
            case UnitTeam.Player:
                attacker.localScale = new Vector3(normalizedX, 1, 1);
                break;
            case UnitTeam.Enemy:
                attacker.localScale = new Vector3(-normalizedX, 1, 1);
                break;
        }
    }
}