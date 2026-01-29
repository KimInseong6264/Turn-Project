using System;
using UnityEngine;

public interface IActable : IAnimatable, IMovable, IAttackable, IKnockbackable
{
    SkillUI UnitSkillUI { get; }
}