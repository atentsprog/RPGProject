using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShortcutButton : MonoBehaviour
{
    Button button;
    public InputAction shortcutKey = new InputAction();
    private void Start()
    {
        button = GetComponent<Button>();
        shortcutKey.performed += ShortcutKey_performed;
        shortcutKey.Enable();
    }

    private void ShortcutKey_performed(InputAction.CallbackContext obj)
    {
        //print("clicked" + shortcutKey.bindings[0]);
        button.onClick.Invoke();
    }
}
