using UnityEngine;

public class Test : MonoBehaviour, ISkillable
{
    public SkillDataSO skillData;
    
    public UnitSkill UnitSkill;
    
    public GameObject target;

    private void Awake()
    {
        UnitSkill = new UnitSkill(skillData,  this);
        GetAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        IHitable a = target.GetComponent<IHitable>();
        StartCoroutine(UnitSkill.UseSkill(a));
    }


    public Animator GetAnimator { get; set; }


    public Transform Move(Transform target, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target.position, 
            speed *  Time.deltaTime
            );
        return transform;
    }

    public void Attack()
    {
        Debug.Log("어택");
    }

    public void Knockback()
    {
        Debug.Log("넉백");
    }
    
    
    public void PlayAni(string animationName)
    {
        GetAnimator.SetTrigger(animationName);
    }
}
