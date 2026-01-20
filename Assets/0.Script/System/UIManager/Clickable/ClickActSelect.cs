using UnityEngine.EventSystems;

public class ClickActSelect : ClickObject, IPointerDownHandler
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 시, 기능을 차단
    }
    
    public void OnPointerDown(PointerEventData eventData) => OnStartCklick();
}