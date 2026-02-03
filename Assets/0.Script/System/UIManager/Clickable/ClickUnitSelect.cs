using UnityEngine;
using UnityEngine.UI;


public class ClickUnitSelect : ClickObject
{
    private bool _isSelectedUnit;
    private Image _image;
    
    public bool IsSelectedUnit =>  _isSelectedUnit;

    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    protected override void OnDisable()
    {
        Init();
        base.OnDisable();
    }

    public override void OnStartCklick()
    {
        ChangeIsSelectedUnit();
        base.OnStartCklick();
    }
    
    
    // 버튼의 설정 세팅
    public void SetButton(UnitDataSO unitData)
    {
        _image.sprite = unitData.UnitSelectButton;
        
        gameObject.name = unitData.name;
        GetComponentInChildren<Text>().text = unitData.name + "Button";
    }
    
    // 버튼 클릭시, 이미지 변화
    private void ChangeIsSelectedUnit()
    {
        if (_isSelectedUnit)
        {
            Color32 newAlpha = Color.white;
            _image.color = newAlpha;
            _isSelectedUnit = false;
        }
        else
        {
            Color32 newAlpha = new Color32(150, 150, 150, 255);
            _image.color = newAlpha;
            _isSelectedUnit = true;
        }
    }

    private void Init()
    {
        _isSelectedUnit = true;
        ChangeIsSelectedUnit();
    }
}