using System;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour, IUnit, IUnitData, IHitable, IAnimatable
{
    public Transform MyTransform =>  transform;

    private void Awake()
    {
        Data = this;
        Hitable = this;
        MyObject = gameObject;
    }

    public void OnHit(BattleInfo battleInfo, int damage, KnockbackInfo? knockbackInfo = null)
    {
        Debug.Log("타격 발생");
    }

    
    
    public IUnitData Data { get; set; }
    public IActable Actable { get; set; }
    public IHitable Hitable { get;set; }
    public GameObject MyObject { get; set; }
    public void SetSkill(SkillType skillType)
    {
        throw new System.NotImplementedException();
    }

    public void StartSkillExecute(BattleInfo battleInfo)
    {
        throw new System.NotImplementedException();
    }

    public string Name { get; }
    public bool IsDead { get; }
    public Dictionary<SkillType, UnitSkill> Skills { get; }
    public Animator MyAnimator { get; }
    
    public void PlayAni(string animationName)
    {
        
    }
}
