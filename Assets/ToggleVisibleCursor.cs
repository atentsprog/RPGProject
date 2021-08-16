using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleVisibleCursor : MonoBehaviour
{
    public InputAction toggleAction = new InputAction("ToggleCursor", InputActionType.Button, "<Keyboard>/tab");
    void Start()
    {
        toggleAction.performed += ToggleVisible;
        toggleAction.Enable();
        ToggleVisible(new InputAction.CallbackContext());
    }

    private void ToggleVisible(InputAction.CallbackContext obj)
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
