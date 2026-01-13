using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitSO", order = 0)]
public class UnitDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Unit UnitPrefab { get; private set; }
}