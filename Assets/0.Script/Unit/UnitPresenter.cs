
public class UnitPresenter
{
    public UnitModel Model { get; private set; }
    public UnitView View { get; private set; }
    
    public UnitPresenter(UnitDataSO unitData,  UnitView view)
    {
        Model = new UnitModel(unitData);
        View = view;
    }
}