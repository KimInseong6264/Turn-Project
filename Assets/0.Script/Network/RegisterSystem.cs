using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;

public class RegisterSystem : MonoBehaviour
{
    
    [SerializeField] private InputField _emailInput;
    [SerializeField] private InputField _passwordInput;
    [SerializeField] private InputField _userName;
    [SerializeField] private Text _message;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _backButton;
    
    public GameObject RegisterUI {get; private set;}

    
    
    private void Start()
    {
        RegisterUI = _emailInput.transform.parent.parent.gameObject;
        
        _createButton.onClick.AddListener(OnRegister);
        _backButton.onClick.AddListener(UpdateInToLogin);
        UpdateRegisterUI(false);
    }
    
    
    
    public void OnRegister() => StartCoroutine(RegisterCor(_emailInput.text, _passwordInput.text));
    
    public void UpdateInToLogin()
    {
        UpdateRegisterUI(false);
        FirebaseDB.Instance.UpdateLoginUI(true);
    }
    
    public void UpdateRegisterUI(bool active) => RegisterUI.SetActive(active);

    // 회원가입 코루틴
    private IEnumerator RegisterCor(string email, string password)
    {
        Task<AuthResult> registerTask = FirebaseDB.Auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            ErrorCheck(registerTask);
        }
        else // 생성 완료
        {
            var user = registerTask.Result.User;
            FirebaseDB.Instance.SetUser(user);
            
            if (FirebaseDB.User != null)
            {
                // 로컬에서 만든 것
                UserProfile profile = new UserProfile { DisplayName = _userName.text };

                // 파이어베이스에 올림
                Task profilTask = user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => profilTask.IsCompleted);

                if (profilTask.Exception != null)
                {
                    Debug.LogWarning("닉네임설정 실패" + profilTask.Exception);
                    _message.text = "닉네임 설정에 실패하였습니다.";
                }
                else
                {
                    _message.text = "생성 완료, 반값습니다" + user.DisplayName + "님";
                    _createButton.interactable = true;
                    yield return CoroutineManager.GetWaitTime(3);
                    
                    UpdateRegisterUI(false);
                    GameManager.Instance.UpdateUI(UIGroupName.GameStart, true);
                }
            }
        }
    }
    
    // 로그인 및 회원가입 실패 시, 에러 출력
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