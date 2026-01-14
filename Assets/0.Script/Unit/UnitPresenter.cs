using UnityEngine;

public class UnitPresenter
{
    public UnitModel Model { get; private set; }
    public UnitView View { get; private set; }
    
    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        View = view;
        Model = new UnitModel(unitData);
        
    }
    
    public void Tick() {}
    
    public void OnAct() {}
    public void OnMove(Vector3 dir) => View.transform.Translate(dir);

    public UnitModel GetModel() => Model;
}