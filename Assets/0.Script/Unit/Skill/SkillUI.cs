using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private CoinSystem _coinSystem;
    [SerializeField] private Text _coinValueText;

    public CoinSystem CoinSystemHandle => _coinSystem;

    private void Start()
    {
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
                rect.localPosition = new Vector3(-rect.position.x, rect.position.y + rect.sizeDelta.y, rect.position.z);
                rect.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }
    
    private void UpdateCoinValueUI()
    {
        _coinValueText.text = _coinSystem.CoinValueSum.ToString();
    }
    
    public void Init() => gameObject.SetActive(false);
}