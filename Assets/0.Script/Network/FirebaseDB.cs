using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;


public class FirebaseDB : SingletonBase<FirebaseDB>
{
    public static FirebaseUser User { get; private set; }   // 인증 된 유저의 정보 전역참조 가능
    public static FirebaseAuth Auth { get; private set; }   // 인증 진행을 위한 객체

    private LoginSystem _loginSystem;
    private RegisterSystem _registerSystem;
    
    
    
    
    protected override void Awake()
    {
        base.Awake();
        _loginSystem = GetComponent<LoginSystem>();
        _registerSystem = GetComponent<RegisterSystem>();
        
        // 비동기 작업은 중간 내역을 확인하기가 어렵다.
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;     // 비동기 작업 결과를 기억시킴
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Auth = FirebaseAuth.DefaultInstance;
                    Debug.Log(Auth);
                    Debug.Log(FirebaseAuth.DefaultInstance);
                    // dbRef = FirebaseDatabase.DefaultInstance.RootReference;
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