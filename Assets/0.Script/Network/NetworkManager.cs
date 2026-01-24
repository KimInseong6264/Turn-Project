using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;


public class NetworkManager : SingletonBase<NetworkManager>
{
    public static FirebaseUser User { get; private set; }   // 인증 된 유저의 정보 전역참조 가능
    public static FirebaseAuth Auth { get; private set; }   // 인증 진행을 위한 객체
    public static DatabaseReference DB { get; private set; } // 데이터베이스 접근

    [SerializeField] private LoginSystem _loginSystem;
    [SerializeField] private RegisterSystem _registerSystem;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        
        // 비동기 작업은 중간 내역을 확인하기가 어렵다.
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;     // 비동기 작업 결과를 기억시킴
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Auth = FirebaseAuth.DefaultInstance;
                    DB = FirebaseDatabase.DefaultInstance.RootReference;
                    Debug.Log("Firebase 동기화 완료");
                }
                else
                {
                    // 비동기 결과가 가능하지 않다면 에러출력
                    Debug.LogError(System.String.Format("뭔가 잘못되었음" + dependencyStatus));
                }
            }
        );
    }
    
    
    
    public void SetUser(FirebaseUser user) =>  User = user;

    public void UpdateLoginUI(bool active) => _loginSystem.UpdateLoginUI(active);
    public void UpdateRegisterUI(bool active) => _registerSystem.UpdateRegisterUI(active);
}