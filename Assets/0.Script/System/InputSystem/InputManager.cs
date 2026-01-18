using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _isAboveUI = false;

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed || _isAboveUI)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out var hit))
        {
            Debug.Log("<color=yellow>오브젝트 클릭감지</color>");
            IClickable target = hit.transform.GetComponent<IClickable>();
            target?.OnStartCklick();
        }
    }

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            _isAboveUI = true;
        else
            _isAboveUI = false;
    }
}