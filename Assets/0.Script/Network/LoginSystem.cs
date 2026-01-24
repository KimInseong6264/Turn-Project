using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
    [SerializeField] private InputField _emailInput;
    [SerializeField] private InputField _passwordInput;
    [SerializeField] private Text _message;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;

    public GameObject LoginUI {get; private set;}

    
    
    private void Start()
    {
        LoginUI = _emailInput.transform.parent.gameObject;
        
        _loginButton.onClick.AddListener(OnLogin);
        _registerButton.onClick.AddListener(UpdateInToRegister);
        StartCoroutine(AutoLoginCor());
    }
    
    
    
    public void UpdateLoginUI(bool active) => LoginUI.SetActive(active);
    
    public void UpdateInToRegister()
    {
        UpdateLoginUI(false);
        NetworkManager.Instance.UpdateRegisterUI(true);
    }
    
    public void OnLogin() => StartCoroutine(LoginCor(_emailInput.text, _passwordInput.text));

    // 자동 로그인
    private IEnumerator AutoLoginCor()
    {
        yield return new WaitUntil(() => NetworkManager.Auth != null);
        
        var auth = NetworkManager.Auth;
        if (auth.CurrentUser != null)
        {
            NetworkManager.Instance.SetUser(auth.CurrentUser);
            _message.color = Color.blue;
            _message.text = "자동 로그인 되었습니다.";
            yield return CoroutineManager.GetWaitTime(2f);
            
            UpdateLoginUI(false);
            GameManager.Instance.UpdateUI(UIGroupName.GameStart, true);
        }
        
        _message.color = Color.gray4;
        _message.text = "이메일과 패스워드를 입력해주세요.";
    }
    
    // 로그인 코루틴
    private IEnumerator LoginCor(string email, string password)
    {
        Task<AuthResult> loginTask = NetworkManager.Auth.SignInWithEmailAndPasswordAsync(email, password);    
    
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);
        
        if (loginTask.Exception != null) // Exception(문제) 가 있을 때
        {
            ErrorCheck(loginTask);
        }
        else
        {
            _message.color = Color.blue;
            NetworkManager.Instance.SetUser(loginTask.Result.User);
            _message.text = "로그인 완료, 반갑습니다" + NetworkManager.User.DisplayName + "님";
            
            
            yield return CoroutineManager.GetWaitTime(2f);
            
            UpdateLoginUI(false);
            GameManager.Instance.UpdateUI(UIGroupName.GameStart, true);
        }
    }
    
    // 로그인 실패 시, 에러 출력
    private void ErrorCheck(Task<AuthResult> registerTask)
    {
        Debug.LogWarning(message: "실패 사유" + registerTask.Exception);
    
        // 에러 확인
        FirebaseException firebaseEx = registerTask.Exception!.GetBaseException() as FirebaseException;
        AuthError errorCode = (AuthError)firebaseEx!.ErrorCode;
        
        _message.color = Color.red;
        switch (errorCode)
        {
            case AuthError.MissingEmail:
                _message.text = "이메일 누락";
                break;
            case AuthError.MissingPassword:
                _message.text = "패스워드 누락";
                break;
            case AuthError.WrongPassword:
                _message.text = "패스워드 틀림";
                break;
            case AuthError.InvalidEmail:
                _message.text = "이메일 형식이 옳지 않음";
                break;
            case AuthError.UserNotFound:
                _message.text = "아이디가 존재하지 않음";
                break;
            default:
                _message.text = "관리자에게 문의 바랍니다";
                break;
        }
    }
}
