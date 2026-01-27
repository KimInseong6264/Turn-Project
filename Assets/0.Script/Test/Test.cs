using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IActable, IUnit, IUnitData, IHitable
{
    public SkillDataSO skillData;

    public UnitSkill UnitSkill;

    public GameObject target;

    
    public IUnitData Data { get; private set; }
    public IActable Actable { get; private set; }
    public IHitable Hitable { get; private set; }
    public GameObject MyObject { get; private set; }
    
    
    private void Awake()
    {
        
        MyAnimator = GetComponent<Animator>();

        Data = this;
        Actable = this;
        Hitable = this;
        MyObject = gameObject;
    }

    private void Start()
    {
        UnitSkill = new UnitSkill(skillData, this);
        IHitable a = target.GetComponent<IHitable>();
        StartCoroutine(UnitSkill.UseSkill(new BattleInfo(this, target.GetComponent<IUnit>(), 5, UnitTeam.Player)));
    }


    public Animator MyAnimator { get; set; }


    public Transform Move(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
        return transform;
    }

    public UnitAttackEvent AttackEvent { get; }

    public void PlayAni(string animationName)
    {
        Debug.Log("플레이 애니: <color=green>" + animationName + "</color>");
        MyAnimator.SetTrigger(animationName);
    }

    public void SetSkill(SkillType skillType)
    {
        throw new System.NotImplementedException();
    }

    public void StartSkillExecute(BattleInfo battleInfo)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(BattleInfo battleInfo)
    {
        battleInfo.Target.Hitable.OnHit(battleInfo, 10);
    }

    public void Knockback(BattleInfo battleInfo)
    {
    }

    
    
    
    
    public string Name { get; }
    public bool IsDead { get; }
    public Dictionary<SkillType, UnitSkill> Skills { get; }
    
    public void OnHit(BattleInfo battleInfo, int damage, KnockbackInfo? knockbackInfo = null)
    {
        throw new System.NotImplementedException();
    }
}