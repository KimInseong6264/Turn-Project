using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance  { get; private set; }

    [SerializeField] private CoroutineManager _coroutine;
    [SerializeField] private SceneChanger _scene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

