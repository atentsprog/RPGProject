using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleVisibleCursor : MonoBehaviour
{
    public InputAction toggleButton = new InputAction("toggleKey", InputActionType.Button, "<Keyboard>/tab");
    void Start() 
    {
        toggleButton.performed += ToggleButton_performed;
        toggleButton.Enable();
    }

    private void ToggleButton_performed(InputAction.CallbackContext obj)
    {
        print("≈« ¥≠∑»¿Ω");
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
