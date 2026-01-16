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
}

