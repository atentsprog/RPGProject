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

        ToggleButton_performed();
    }
     
    private void ToggleButton_performed(InputAction.CallbackContext obj = default)
    { 
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
