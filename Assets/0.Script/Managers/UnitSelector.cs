
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public static UnitSelector Instance;

    public List<UnitDataSO> Players;
    public List<UnitDataSO> Enemies;

    private void Awake()
    {
        Players = new List<UnitDataSO>();
        Enemies = new List<UnitDataSO>();
    }

    public void SetPlayers(UnitDataSO unitData)
    {
        Players.Add(unitData);
    }  
    public void SetEnemies(UnitDataSO unitData)
    {
        Enemies.Add(unitData);
    }
    
    
    //
    // public UnitDataSO GetUnitData(string unitName)
    // {
    //     return _unitDataList[unitName];
    // }
}