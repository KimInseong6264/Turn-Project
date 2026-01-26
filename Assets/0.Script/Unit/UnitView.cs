using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour, ISkillable, IClickable
{
    [SerializeField] private Slider _hpBar;
    private Animator _animator;
    
    public UnitDataSO UnitData;
    
    public UnitPresenter Presenter { get; private set; }
    
    public Animator GetAnimator =>  _animator;
    
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
    // private void OnDisable()
    // {
    //     OnClick -= OnStartCklick;
    // }

    

    public void OnStartCklick()
    {
        Debug.Log("클릭대상" + UnitData.Name);
    }

    public void UpdateHpBar(float hp, float maxHp) => _hpBar.value = hp / maxHp;
    public void SetActiveHpBar(bool active) => _hpBar.gameObject.SetActive(active);


    public void PlayAni(string animationName)
    {
        _animator.Play(animationName);
    }

    public void Attack()
    {
        throw new NotImplementedException();
    }

    public void Knockback()
    {
        throw new NotImplementedException();
    }


    public Transform Move(Transform target, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target.position, 
            speed * Time.deltaTime
            );
        return transform;
    }
}
