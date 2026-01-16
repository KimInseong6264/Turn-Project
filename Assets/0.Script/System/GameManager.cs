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

    public void OnLoadScene(int num) => _sceneMgr.OnLoadScene(num);
    public void UpdateUI(UIGroupName uiGroupName, bool active) => _uiMgr.UpdateUI(uiGroupName, active);
    public UnitDataSO GetUnitData(string unitName) => _unitDataMgr.GetUnitData(unitName);
    public Dictionary<string, UnitDataSO> GetUnitDataList() => _unitDataMgr.GetUnitDataList();

    public void InitButtons(bool isClear) => _uiMgr.InitButtonList(isClear);
    public T CreateButton<T>(UIGroupName uiGroupName, string createTag = "") where T : ClickObject
    {
        return _uiMgr.OnCreateButton<T>(uiGroupName, createTag);
    }
}

