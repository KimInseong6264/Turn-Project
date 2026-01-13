using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private CoroutineManager _coroutine;
    [SerializeField] private SceneChanger _scene;

    protected override void Awake()
    {
        base.Awake();
    }
    
}

