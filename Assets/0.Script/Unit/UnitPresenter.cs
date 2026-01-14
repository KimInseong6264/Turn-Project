using UnityEngine;

public class UnitPresenter
{
    private UnitModel _model;

    private UnitView _view;
    
    public string Name => _model.Name;
    public int Speed => _model.Speed;
    
    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        _view = view;
        _model = new UnitModel(unitData);
        
    }
    
    public void Tick() {}
    
    public void OnAct() {}
    public void OnMove(Vector3 dir) => _view.transform.Translate(dir);
    
    public void SetPosition(Vector3 pos) => _view.transform.position = pos;
    public void SetObjectName(string name) => _view.gameObject.name = name;
    
}