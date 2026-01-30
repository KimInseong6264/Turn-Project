using System;
using System.Collections.Generic;

public interface ISpawn
{
    event Action<List<UnitPresenter>> OnSpawningCompleted;
    
    void OnSpawn(List<UnitDataSO> unitDataList);
}

public interface IUnitSetPos
{
    void SetUnitsPosition(List<UnitPresenter> unitList);
}
