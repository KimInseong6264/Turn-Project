using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _IsInput;
    private RaycastHit _hit;

    public void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        _IsInput = Physics.Raycast(ray, out _hit);
    }

    private void Update()
    {
        if (_IsInput)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI를 클릭");
                _IsInput = false;
                return;
            }

            Debug.Log("<color=yellow>클릭감지</color>");
            IClickable target = _hit.transform.GetComponent<IClickable>();
            target?.OnCklick();

            _IsInput = false;
        }
    }
}