using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    private Vector3 _intialPos;
    [SerializeField] private CoinSystem _coinSystem;
    [SerializeField] private Text _coinValueText;

    public CoinSystem CoinSystemHandle => _coinSystem;

    private void Start()
    {
        _intialPos = transform.GetComponent<RectTransform>().localPosition;
        _coinSystem.OnNotifyCoinToss += UpdateCoinValueUI;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _coinSystem.OnNotifyCoinToss -= UpdateCoinValueUI;
    }

    public void OnSkillUI(IUnit unit)
    {
        SetSkillUI(unit);
        gameObject.SetActive(true);
    }

    private void SetSkillUI(IUnit unit)
    {
        RectTransform rect =  transform.GetComponent<RectTransform>();
        switch (unit.Data.Team)
        {
            case UnitTeam.Player:
                break;
            case UnitTeam.Enemy:
                rect.localPosition = new Vector3(-rect.localPosition.x, rect.localPosition.y, rect.localPosition.z);
                rect.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }
    
    private void UpdateCoinValueUI()
    {
        _coinValueText.text = _coinSystem.CoinValueSum.ToString();
    }

    public void Init()
    {
        RectTransform rect =  transform.GetComponent<RectTransform>();
        rect.localPosition = _intialPos;
        rect.localScale = new Vector3(1, 1, 1);
        
        _coinValueText.text = "0";
        gameObject.SetActive(false);
    }
    
}