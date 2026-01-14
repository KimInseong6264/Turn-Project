using System;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    private UnitPresenter _presenter;
    
    //
    public UnitDataSO unitData;
    //
    
    private void Awake()
    {
        _presenter = new UnitPresenter(unitData, this);
    }
}