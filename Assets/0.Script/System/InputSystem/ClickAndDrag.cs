using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 드래깅엔 3가지 스텝이 있음, 드래깅 시작, 드래깅 중, 드래그 종료
/// </summary>
public class ClickAndDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _arrowPrefab;

    private RectTransform _arrowRect;
    private RectTransform _spawnTransform;
    private List<RaycastResult> _raycastResults;

    private void Awake()
    {
        _raycastResults = new List<RaycastResult>();
    }

    // 드래깅이 시작될 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        CreateArrow();
    }

    //드래그 중일 땐, 위치 정보 등 드래깅에 필요한 모든 정보가 eventData에 들어있음
    public void OnDrag(PointerEventData eventData)
    {
        UpdateArrow(eventData);
    }

    // 드래그 끝날 때(마우스를 뗄 때
    public void OnEndDrag(PointerEventData eventData)
    {
        ClickObject button = FindedClickObject(eventData);
        button?.OnStartCklick();
        
        Destroy(_arrowRect.gameObject);
    }

    private void CreateArrow()
    {
        RectTransform myRect = transform as RectTransform;
        if(!myRect)
            return;
        
        _arrowRect = Instantiate(_arrowPrefab, transform).GetComponent<RectTransform>();
        _arrowRect.anchoredPosition = Vector2.zero;
    }

    private void UpdateArrow(PointerEventData eventData)
    {
        
        RectTransform parentRect = _arrowRect.parent as RectTransform;
        // 마우스 좌표를 UI 기준으으로 변경
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            eventData.pressEventCamera, 
            out Vector2 mouseRectPos
            );

        Vector2 dir = mouseRectPos;
        _arrowRect.up = dir;
        dir /= _arrowRect.localScale.x;
        _arrowRect.sizeDelta = new Vector2(_arrowRect.sizeDelta.x, dir.magnitude);
    }

    private ClickObject FindedClickObject(PointerEventData eventData)
    {
        EventSystem.current.RaycastAll(eventData, _raycastResults);
        foreach (RaycastResult result in _raycastResults)
        {
            if (result.gameObject.TryGetComponent<ClickObject>(out var clickObject))
                return clickObject;
        }
        return null;
    }
}