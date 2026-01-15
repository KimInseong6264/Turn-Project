using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 드래깅엔 3가지 스텝이 있음, 드래깅 시작, 드래깅 중, 드래그 종료
/// </summary>
public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _cubePrefab;
    
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();  // 물론 미리 만들어진 캔버스 그룹 정보 담아도 됨
    }

    // 드래깅이 시작될 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;
        transform.SetParent(_originalParent.root);
        
        _canvasGroup.alpha = 0.5f;
        
        _canvasGroup.blocksRaycasts = false;
    }


    //드래그 중일 땐, 위치 정보 등 드래깅에 필요한 모든 정보가 eventData에 들어있음
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // 검증, 위치확인, 패널에 놓기, 이미지 남기기, 페어런트 변경, 아니면 돌아간다
    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        // 드롭이 감지되지 않았다면? 원래 위치로 복귀
        if (transform.parent == _originalParent.root)
        {
            transform.SetParent(_originalParent);
        }
        //
        // if (IsDroppedOnUI(eventData))
        //     transform.SetParent(_originalParent);
    }

    // UI에 드롭시켰는지 판단할 메서드
    private bool IsDroppedOnUI(PointerEventData eventData) => 
        eventData.pointerEnter != null && 
        eventData.pointerEnter.transform.IsChildOf(_originalParent.root);
}