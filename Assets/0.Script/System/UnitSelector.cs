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
    public static void SelectedUnit(string unitName)
    {
        Players.Add(GameManager.Instance.GetUnitData(unitName));
        Debug.Log(unitName + "선택되었습니다.");
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