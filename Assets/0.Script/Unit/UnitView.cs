using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour, IClickable
{
    [SerializeField] private Slider _hpBar;
    
    public UnitDataSO UnitData;
    public UnitPresenter Presenter { get; private set; }
    public event Action OnClick;
    
    
    
    private void Awake()
    {
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
    
    private void OnEnable()
    {
        OnClick += OnStartCklick;
    }
    private void OnDisable()
    {
        OnClick -= OnStartCklick;
    }

    

    public void OnStartCklick()
    {
        Debug.Log("클릭대상" + UnitData.Name);
    }

    public void UpdateHpBar(float hp, float maxHp) => _hpBar.value = hp / maxHp;
    public void SetActiveHpBar(bool active) => _hpBar.gameObject.SetActive(active);
}
