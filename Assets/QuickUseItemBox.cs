using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ItemBox))]
public class QuickUseItemBox : MonoBehaviour, IDropHandler
{
    ItemBox itemBox;
    Text shortcutNumber;

    [SerializeField] InputAction inputAction;

    private void Awake()
    {
        shortcutNumber = transform.Find("Number").GetComponent<Text>();
        itemBox = GetComponent<ItemBox>();
        itemBox.LinkComponent();
        itemBox.button.onClick.AddListener(() => UseSkill());
    }

    private void UseSkill()
    {
        print("UseSkill " + shortcutNumber.text);
        if(itemBox.inventoryItemInfo!= null)
            print(itemBox.inventoryItemInfo.ItemInfo.name);
    }

    public int index;
    public void Init(int _index, string bindingString)
    {
        index = _index;
        inputAction = new InputAction("Key", InputActionType.Button, bindingString);
        inputAction.performed += InputAction_performed;
        inputAction.Enable();

        shortcutNumber.text = bindingString.Replace("<Keyboard>/", "");
    }

    private void InputAction_performed(InputAction.CallbackContext obj)
    {
        UseSkill();
    }

    public void SetItem(InventoryItemInfo inventoryItemInfo)
    {
        itemBox.Init(inventoryItemInfo);
    }

    public void OnDrop(PointerEventData data)
    {
        ItemBox fromItemBox = data.pointerDrag.GetComponent<ItemBox>();

        SetItem(fromItemBox.inventoryItemInfo);
        int uid = 0;
        if (fromItemBox.inventoryItemInfo != null)
            uid = fromItemBox.inventoryItemInfo.uid;
        UserData.Instance.itemData.data.shortcutItemUIDs[index] = uid;
    }
}
