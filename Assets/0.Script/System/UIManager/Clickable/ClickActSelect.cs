using UnityEngine;
using UnityEngine.EventSystems;

public class ClickActSelect : ClickObject, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _initialScale;
    private bool _isClick;


    private void Awake()
    {
        _initialScale =  gameObject.GetComponent<RectTransform>().localScale;
    }

    public override void OnPointerClick(PointerEventData eventData) {}
    

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClick = true;
        OnStartCklick();
    }

    public void OnPointerUp(PointerEventData eventData) => Init();
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Init();
        ChangeButton();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isClick)
            Init();
    }
    

    private void ChangeButton()
    {
        var buttonRect = gameObject.GetComponent<RectTransform>();
        Vector3 scaleUp = _initialScale * 1.2f;
        buttonRect.localScale = scaleUp;
    }

    private void Init()
    {
        var buttonRect = gameObject.GetComponent<RectTransform>();
        buttonRect.localScale = _initialScale;
        _isClick = false;
    }



}