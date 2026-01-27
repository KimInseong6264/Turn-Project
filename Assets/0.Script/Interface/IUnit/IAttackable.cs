
using UnityEngine;

public interface IAttackable
{
    GameObject MyObject { get; }
    
    void Attack(BattleInfo battleInfo);
    
}