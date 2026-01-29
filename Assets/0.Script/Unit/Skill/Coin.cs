using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// 다른 클래스에서는 코인 시스템을 new로 받아오기만 하면 됨
public class Coin : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Init();
    }



    public CoinType GetTossResult()
    {
        var result = (CoinType)Random.Range(0, 2);
        ChangeAnimation(result);
        return result;
    }

    private void ChangeAnimation(CoinType coinType)
    {
        var mine = GetComponent<Image>();
        switch (coinType)
        {
            case CoinType.Back:
                mine.color = Color.red;
                // _animator?.Play("Back");
                break;
            case CoinType.Front:
                mine.color = Color.yellow;
                // _animator?.Play("Front");
                break;
        }
    }
    
    private void Init()
    {
        // _animator?.Play("Tossing");
    }
}

public enum CoinType
{
    Back, Front
}