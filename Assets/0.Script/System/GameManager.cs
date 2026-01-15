using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SceneChanger _sceneMgr;
    [SerializeField] private UnitDataManager _unitDataMgr;
    [SerializeField] private InputManager _inputMgr;
    
    protected override void Awake()
    {
        base.Awake();
    }

    public void OnLoadScene(int num) => _sceneMgr.OnLoadScene(num);
    public UnitDataSO GetUnitData(string unitName) => _unitDataMgr.GetUnitData(unitName);
}

