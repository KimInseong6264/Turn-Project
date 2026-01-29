using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitAttackEvent))]
public class UnitView : MonoBehaviour, IActable, IClickable
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private SkillUI _unitSkillUI;
    
    private Animator _animator;
    
    public UnitDataSO UnitData;
    
    public GameObject MyObject => gameObject;
    public Animator MyAnimator => _animator;
    public SkillUI UnitSkillUI => _unitSkillUI;
    
    public UnitPresenter Presenter { get; private set; }
    
    
    public event Action OnClick;
    
    
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Presenter = new UnitPresenter(UnitData, this);

        if (_hpBar == null)
            Debug.LogWarning(UnitData.Name + "HpBar 세팅해!");
        else
            _hpBar.value = 1;
    }

    private void Update()
    {
        Presenter.Tick();
    }
    
    // private void OnEnable()
    // {
    //     OnClick += OnStartCklick;
    // }
    private void OnDisable()
    {
        // OnClick -= OnStartCklick;
    }

    

    public void OnStartCklick()
    {
        Debug.Log("클릭대상" + UnitData.Name);
    }

    public void UpdateHpBar(float hp, float maxHp) => _hpBar.value = hp / maxHp;
    public void SetActiveHpBar(bool active) => _hpBar.gameObject.SetActive(active);


    public void PlayAni(string animationName)
    {
        Debug.Log("플레이 애니: <color=green>" + animationName + "</color>");
        _animator.Play(animationName);
    }

    public void Attack(BattleInfo battleInfo, int finalCoinValue)
    {
        var target = battleInfo.Target;
        target.Hitable.OnHit(battleInfo, finalCoinValue);
    }

    public void Knockback(BattleInfo battleInfo)
    {
        
    }


    public Transform Move(Vector3 targetPos, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            targetPos, 
            speed * Time.deltaTime
            );
        return transform;
    }
}
