using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _IsInput;

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if(!ctx.performed)
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
}