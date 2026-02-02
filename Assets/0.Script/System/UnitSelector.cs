using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public static List<UnitDataSO> Players { get; private set; }
    public static List<UnitDataSO> Enemies { get; private set; }

    
    
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        Enemies = GameManager.Instance.GetWaveEnemyList();
    }


    // 유닛 선택
    public static void SelectedUnit(UnitDataSO unit, bool isSelectedUnit)
    {
        switch (isSelectedUnit)
        {
            case true:
                Players.Add(unit);
                Debug.Log(unit.Name + "선택되었습니다.");
                break;
            case false:
                Players.Remove(unit);
                break;
        }
    }

    // 유닛 선택 취소
    public static void CanceledUnit(string unitName)
    {
        UnitTeam unitTeam = GameManager.Instance.GetUnitData(unitName).Team;
        switch (unitTeam)
        {
            case UnitTeam.Player:
                Players.Remove(GameManager.Instance.GetUnitData(unitName));
                break;
            case UnitTeam.Enemy:
                Enemies.Remove(GameManager.Instance.GetUnitData(unitName));
                break;
        }
    }
    
    public void Init()
    {
        if (Players != null && Enemies != null)
        {
            Players.Clear();
            Enemies.Clear();
            return;
        }
        
        Players = new List<UnitDataSO>();
        Enemies = new List<UnitDataSO>();
    }
}