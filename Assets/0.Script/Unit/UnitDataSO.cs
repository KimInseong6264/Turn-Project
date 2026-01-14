using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitSO", order = 0)]
public class UnitDataSO : ScriptableObject
{

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Faction Team { get; private set; }
    [field: SerializeField] public int Hp { get; private set; }
    [field: SerializeField] public float AttLevel { get; private set; }
    [field: SerializeField] public float DefLevel { get; private set; }
    [field: SerializeField] public int Speed { get; private set; }
    [field:SerializeField] public UnitView UnitPrefab { get; private set; }

}

public enum Faction
{
    Player, Enemy
}