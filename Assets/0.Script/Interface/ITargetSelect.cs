using System;
using System.Collections.Generic;

public interface ITargetSelect
{
    event Action<BattleInfo> OnTargetSelected;
    event Action<BattleInfo> OnResetTargetSelected;
    
    public Dictionary<string, BattleInfo> BattleSequence { get; }
    
    public void SetSequenceTarget(UnitPresenter target);
}