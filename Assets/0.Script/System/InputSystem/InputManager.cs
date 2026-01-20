using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _isAboveUI = false;

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || _isAboveUI)
            return;

        if (!Camera.main)
            return;
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (!Physics.Raycast(ray, out var hit)) 
            return;
        
        Debug.Log("<color=yellow>오브젝트 클릭감지</color>");
        IClickable target = hit.transform.GetComponent<IClickable>();
        target?.OnStartCklick();
    }

    private void Update()
    {
        _isAboveUI = EventSystem.current.IsPointerOverGameObject();
    }
}