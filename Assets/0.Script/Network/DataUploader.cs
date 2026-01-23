using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;

public class DataUploader : MonoBehaviour
{
    public UnitDataSO[] UnitDataSos;
    
    private DatabaseReference _database;

    private void Start()
    {
        _database = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(_database + "세팅 완료");
    }

    [ContextMenu("유닛 업로드")]
    public void UploadAll()
    {
        foreach (var unitData in UnitDataSos)
        {
            StartCoroutine(UploadData(unitData));
        }
    }

    private IEnumerator UploadData(UnitDataSO unitData)
    {
        UnitDataDTO unitDTO = new UnitDataDTO(unitData);
        string json = JsonUtility.ToJson(unitDTO);
        var DBTask = _database.Child("metadata").Child("units").Child(unitData.name)
            .SetRawJsonValueAsync(json);
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"업로드 실패! \n사유 {DBTask.Exception}");
        }
        else
        {
            Debug.Log($"<color=yellow>{unitData.name}</color> 업로드!");
        }
    }
}