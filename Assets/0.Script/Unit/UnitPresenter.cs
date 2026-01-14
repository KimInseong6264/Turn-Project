using UnityEngine;

public class UnitPresenter
{
    private UnitModel _model;
    
    public UnitView View { get; private set; }
    
    public string Name => _model.Name;
    public int Speed => _model.Speed;
    
    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        View = view;
        _model = new UnitModel(unitData);
        
    }
    
    public void Tick() {}
    
    public void OnAct() {}
    public void OnMove(Vector3 dir) => View.transform.Translate(dir);

    public UnitModel GetModel() => _model;
}