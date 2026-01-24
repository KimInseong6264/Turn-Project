using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class UnitDataManager : MonoBehaviour
{
    [SerializeField] private List<UnitDataSO> _unitDataList;

    private Dictionary<string, UnitDataSO> _unitDataDict;
    
    
    
    private void Awake()
    {
        _unitDataDict = new Dictionary<string, UnitDataSO>();
        SetUnitData();
    }

    private void Start()
    {
        StartCoroutine(SyncDataRoutine());
    }


    // 유닛Dict에 데이터를 세이브
    private void SetUnitData()
    {
        foreach (var unitData in _unitDataList)
        {
            SetPrefabUnitData(unitData);
            
            _unitDataDict.TryAdd(unitData.Name, unitData);
        }
    }

    // SO의 프리펩UnitView에 SO데이터를 끼워넣음
    private void SetPrefabUnitData(UnitDataSO unitData)
    {
        if (unitData.UnitPrefab == null)
            Debug.LogWarning($"{unitData.name}의 SO에 프리팹 없음");
        else
            unitData.UnitPrefab.GetComponent<UnitView>().UnitData =  unitData;
    }
    
    public Dictionary<string, UnitDataSO> GetUnitDataList() => _unitDataDict;
    public UnitDataSO GetUnitData(string unitName) => _unitDataDict[unitName];
    
    // DB의 데이터로 데이터도감 갱신
    private IEnumerator SyncDataRoutine()
    {
        yield return new WaitUntil(() => NetworkManager.DB != null);
        Debug.Log("서버 데이터 동기화 시작...");

        // 1. Firebase에 데이터 요청 (Task 생성)
        var task = NetworkManager.DB.Child("metadata").Child("units").GetValueAsync();

        // 2. [WaitUntil] 데이터가 올 때까지 여기서 멈춰서 기다림
        yield return new WaitUntil(() => task.IsCompleted);

        // 3. 결과 처리
        if (task.Exception != null)
        {
            Debug.LogError("데이터 로드 실패: " + task.Exception);
        }
        else if (task.Result.Value == null)
        {
            Debug.LogWarning("저장된 데이터가 없습니다");
        }
        else
        {
            DataSnapshot snapshot = task.Result;
            
            // 4. 받아온 데이터들을 순회하며 SO에 매칭
            foreach (DataSnapshot unitData in snapshot.Children)
            {
                string json = unitData.GetRawJsonValue();
                UnitDataDTO serverData = JsonUtility.FromJson<UnitDataDTO>(json);

                // SO 리스트에서 ID(파일이름)가 일치하는 녀석 찾기
                if (_unitDataDict.TryGetValue(serverData.Name, out var targetSO))
                {
                    ApplyDataToSO(serverData, targetSO);
                    Debug.Log($"{targetSO.name} 데이터 갱신 완료!");
                }
            }
        }
        
        Debug.Log("<color=green>모든 데이터 동기화 프로세스 종료.</color>");
    }

    // DTO의 데이터를 실제 SO 필드에 복사하는 메서드
    private void ApplyDataToSO(UnitDataDTO dto, UnitDataSO so)
    {
        so.SetSOValue(dto);
        
        // 스킬리스트 매칭
        foreach (SkillDataDTO skillDTO in dto.SkillList)
        {
            foreach (SkillDataSO skillSO in so.SkillList)
            {
                if(skillSO.Name == skillDTO.Name)
                    skillSO.SetSOValue(skillDTO);
            }
        }
    }
}