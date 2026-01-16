
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

    // 유닛 선택
    public void SelectedUnit(string unitName)
    {
        UnitTeam unitTeam = GameManager.Instance.GetUnitData(unitName).Team;
        switch (unitTeam)
        {
            case UnitTeam.Player:
                Players.Add(GameManager.Instance.GetUnitData(unitName));
                break;
            case UnitTeam.Enemy:
                Enemies.Add(GameManager.Instance.GetUnitData(unitName));
                break;
        }
    }

    // 유닛 선택 취소
    public void CanceledUnit(string unitName)
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
    
    // 전투 씬으로 전환
    public void BattleLoadScene()
    {
        GameManager.Instance.OnLoadScene(1);
        gameObject.SetActive(false);
    }
    
    
    private void Init()
    {
        if (Players != null && Enemies != null)
        {
            Players.Clear();
            Enemies.Clear();
            Debug.LogWarning("셀렉트 초기화");
            return;
        }
        
        Debug.LogWarning("셀렉트 뉴할당");
        Players = new List<UnitDataSO>();
        Enemies = new List<UnitDataSO>();
    }
}