using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] private SceneChanger _scene;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OnLoadScene(int num) => _scene.OnLoadScene(num);
}

