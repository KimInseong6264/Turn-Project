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

        Debug.Log("<color=yellow>클릭감지</color>");
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out var hit))
        {
            IClickable target = hit.transform.GetComponent<IClickable>();
            target?.OnCklick();
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