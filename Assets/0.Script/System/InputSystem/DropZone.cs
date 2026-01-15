using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //드래그중이던 대상의 부모를, 이걸로 바꾼다.
            eventData.pointerDrag.transform.SetParent(transform);
        }
    }
}