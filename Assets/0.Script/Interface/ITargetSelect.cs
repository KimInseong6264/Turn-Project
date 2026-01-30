using System;
using System.Collections.Generic;

public interface ITargetSelect
{
    event Action<BattleInfo> OnTargetSelected;
    
    public Dictionary<string, BattleInfo> BattleSequence { get; }
    
    public void SetSequenceTarget(UnitPresenter target);
}