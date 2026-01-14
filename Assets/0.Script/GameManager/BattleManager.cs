using System.Collections.Generic;
using UnityEngine;


public class BattleManager : SingletonBase<BattleManager>
{
    private List<UnitModel> _players;
    private List<UnitModel> _enemies;
    
    protected override void Awake()
    {
        base.Awake();
        
    }
}