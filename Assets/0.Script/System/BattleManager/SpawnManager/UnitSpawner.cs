using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour, ISpawn, IUnitSetPos
{
    public event Action<List<UnitPresenter>> OnSpawningCompleted;

    private List<UnitPresenter> _createdUnits;
    [SerializeField] private float _distance = 4;
    
    
    private void Awake()
    {
        _createdUnits = new List<UnitPresenter>();
    }
    
    
    // 유닛리스트 숫자에 맞는 Spawn을 진행
    public void OnSpawn(List<UnitDataSO> unitDataList)
    {
        _createdUnits.Clear();
        
        foreach (var unitData in unitDataList)
        {
            UnitPresenter presenter = CreateUnit(unitData);
            _createdUnits.Add(presenter);
        }
        
        SetUnitsPosition(_createdUnits);
        OnSpawningCompleted?.Invoke(_createdUnits);
    }
    
    // 유닛 생성
    private UnitPresenter CreateUnit(UnitDataSO unitData)
    {
        UnitPresenter presenter = Instantiate(unitData.UnitPrefab).Presenter;
        presenter.SetObjectName(unitData.Name);
        return presenter;
    }
    
    // 유닛 리스트를 받으면 위치를 수정해주는 메서드
    public void SetUnitsPosition(List<UnitPresenter> unitList)
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        
        switch (unitList.Count)
        {
            case 1:
                unitList[0].MyTransform.position = transform.position;
                break;
            
            case 2:
                unitList[0].MyTransform.position = new Vector3(x + _distance, y, z);
                unitList[1].MyTransform.position =  new Vector3(x + _distance / 2 , y + _distance / 2, z);
                break;
            
            case 3:
                float distanceX = _distance * Mathf.Cos(30 * Mathf.Deg2Rad);
                float distanceZ = _distance * Mathf.Sin(30 * Mathf.Deg2Rad);
                unitList[0].MyTransform.position = new Vector3(x - distanceX, y, z - distanceZ);
                unitList[1].MyTransform.position =  new Vector3(x + ( distanceX / 2), y, z + distanceZ);
                unitList[2].MyTransform.position = new Vector3(x + distanceX, y, z - distanceZ);
                break;
            
            default:
                Debug.Log("아직 미구현");
                break;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}