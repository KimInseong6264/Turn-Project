using System.Collections;
using Firebase.Database;
using UnityEngine;

public class DBSystem : MonoBehaviour
{
    
    IEnumerator UpdateMoney(int money)
    {
        DatabaseReference dbRef = NetworkManager.DB;
        
        // 마치 속에 user라는 이름의 폴더를 만들 듯, 하위를 만들고 SetValueAsync로 money를 기억
        var DBTask = dbRef.Child("users").Child(NetworkManager.User.UserId).Child("money").SetValueAsync(money);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"돈 업데이트 실패! 사유 {DBTask.Exception}");
        }
        else
        {
            // 만약 저장완료 팝업 같은 거 띄우고 싶다면 여기 작성
            // 저장완료를 3초간 띄위고, 페이드 아웃
        }
    }
}
