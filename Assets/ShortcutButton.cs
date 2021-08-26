using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShortcutButton : MonoBehaviour
{
    [SerializeField] InputAction inputAction = new InputAction();
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        inputAction.Enable();
        inputAction.performed += InputAction_performed;
    }

    private void InputAction_performed(InputAction.CallbackContext obj)
    {
        print("clicked");
        button.onClick.Invoke();
    }
}
