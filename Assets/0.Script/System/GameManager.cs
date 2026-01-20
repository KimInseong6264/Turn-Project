using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SceneChanger _sceneMgr;
    [SerializeField] private UnitDataManager _unitDataMgr;
    [SerializeField] private InputManager _inputMgr;
    [SerializeField] private UIManager _uiMgr;
    [SerializeField] private UnitSelector _unitSelector;
    [SerializeField] private WaveManager _waveMgr;
    
    
    
    protected override void Awake()
    {
        base.Awake();
    }

    
    
    // Scene 메서드
    public void OnLoadScene(int num) => _sceneMgr.OnLoadScene(num);
    
    // UnitData 메서드
    public UnitDataSO GetUnitData(string unitName) => _unitDataMgr.GetUnitData(unitName);
    public Dictionary<string, UnitDataSO> GetUnitDataList() => _unitDataMgr.GetUnitDataList();

    // UI 메서드
    public void UpdateUI(UIGroupName uiGroupName, bool active) => _uiMgr.UpdateUI(uiGroupName, active);
    
    // Wave 메서드
    public List<UnitDataSO> GetWaveEnemyList() => _waveMgr.GetEnemyDataList();

    // 전역참조 캡슐화 초기화
    public void GameInitialIze()
    {
        _uiMgr.Init();
        _unitSelector.Init();
    }
    
    // 게임 종료 메서드
    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}

