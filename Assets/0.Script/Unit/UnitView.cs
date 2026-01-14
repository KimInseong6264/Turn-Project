using System;
using UnityEngine;

public class UnitView : MonoBehaviour
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
}