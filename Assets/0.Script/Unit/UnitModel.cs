

public class UnitModel
{
    public string Name { get; private set; }
    
    public UnitModel(UnitDataSO unitData)
    {
        Name = unitData.Name;
    }
}