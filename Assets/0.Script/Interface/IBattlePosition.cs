using UnityEngine;

public interface IBattlePosition
{
    Vector3 PlayerPos { get; }
    Vector3 EnemyPos { get; }
}