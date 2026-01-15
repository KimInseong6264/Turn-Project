using System;
using UnityEngine;

public class UnitView : MonoBehaviour, IClickable
{
    public UnitPresenter Presenter { get; private set; }
    
    //
    public UnitDataSO unitData;
    //
    
    private void Awake()
    {
        Presenter = new UnitPresenter(unitData, this);
    }


    private void Update()
    {
        Presenter.Tick();
    }

    public void SetSkill()
    {
    }


    public void OnCklick()
    {
        Debug.Log("클릭대상" + unitData.Name);
    }
}