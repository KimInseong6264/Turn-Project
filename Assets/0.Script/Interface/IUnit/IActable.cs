using System;
using UnityEngine;

public interface IActable : IAnimatable, IMovable, IAttackable, IKnockbackable
{
    SkillUI UnitSkillUI { get; }
}

public interface IMovable
{
    Transform Move(Vector3 targetPos, float speed);
}

public interface IAttackable
{
    GameObject MyObject { get; }
    
    void Attack(BattleInfo battleInfo, int finalCoinValue);
}

public interface IKnockbackable
{
    void Knockback(BattleInfo battleInfo);
}