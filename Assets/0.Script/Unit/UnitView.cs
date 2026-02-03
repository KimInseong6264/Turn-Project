using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitAttackEvent))]
public class UnitView : MonoBehaviour, IActable, IClickable
{
    private UnitDataSO _unitData;
    private Animator _animator;
    private Text _hpText;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private SkillUI _unitSkillUI;
    
    
    public UnitDataSO UnitData => _unitData;
    public GameObject MyObject => gameObject;
    public Animator MyAnimator => _animator;
    public SkillUI UnitSkillUI => _unitSkillUI;
    
    public UnitPresenter Presenter { get; private set; }
    
    
    public event Action OnClick;
    
    
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_hpBar == null)
            Debug.LogWarning(_unitData.Name + "HpBar 세팅해!");
        else
        {
            Transform hpCount = _hpBar.transform.Find("HpCount");
            _hpText = hpCount.GetComponentInChildren<Text>();
        }
    }

    private void Start()
    {
        Presenter.Initailize();
    }

    // private void Update()
    // {
    //     Presenter.Tick();
    // }
    
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
        Debug.Log("클릭대상" + _unitData.Name);
    }

    public void OnCreatePresenter(UnitDataSO unitData)
    {
        _unitData = unitData;
        Presenter = new UnitPresenter(_unitData, this);
    }

    public void UpdateHpBar(float hp, float maxHp) => _hpBar.value = hp / maxHp;
    public void UpdateHpText(int hp) => _hpText.text = hp.ToString();
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
