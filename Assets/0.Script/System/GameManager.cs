using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SceneChanger _scene;
    [SerializeField] private UnitDataManager _unitData;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OnLoadScene(int num) => _scene.OnLoadScene(num);
    public UnitDataSO GetUnitData(string unitName) => _unitData.GetUnitData(unitName);
}

