using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitAttackEvent))]
public class UnitView : MonoBehaviour, IActable
{
    private UnitDataSO _unitData;
    private Animator _animator;
    private Text _hpText;
    private SpriteRenderer _sprite;
    private float _timer;
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
        _sprite = GetComponent<SpriteRenderer>();

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

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 0.1f)
        {
            SetLayer();
            _timer = 0;
        }
    }
    

    public void OnCreatePresenter(UnitDataSO unitData)
    {
        _unitData = unitData;
        Presenter = new UnitPresenter(_unitData, this);
    }

    
    private void SetLayer()
    {
        int y = (int)(-100 * transform.position.y);
        if(_sprite.sortingOrder != y)
            _sprite.sortingOrder = y;
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
