using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SceneChanger _sceneMgr;
    [SerializeField] private UnitDataManager _unitDataMgr;
    [SerializeField] private InputManager _inputMgr;
    [SerializeField] private UIManager _uiMgr;
    [SerializeField] private UnitSelector _unitSelector;
    
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
}

