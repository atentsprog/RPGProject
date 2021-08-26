using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ItemBox))]
[RequireComponent(typeof(ShortcutButton))]
public class QuickItemUseBox : MonoBehaviour, IDropHandler
{
    public ItemBox itembox;
    public Text number;
    public void OnDrop(PointerEventData eventData)
    {
        print(eventData);
        ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
        itembox.Init(fromItemBox.inventoryItemInfo);

        int itemUid = fromItemBox.inventoryItemInfo.uid;
        UserData.Instance.itemData.data.quickItemUIDs[index] = itemUid;
    }

    int index;
    internal void Init(int _index, InventoryItemInfo inventoryItemInfo
        , string keybindingString)
    {
        index = _index;
        itembox.Init(inventoryItemInfo);

        number.text = keybindingString.Replace("<Keyboard>/", "");

        GetComponent<ShortcutButton>().shortcutKey
            = new InputAction("key", InputActionType.Button, keybindingString);
        GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        print(number.text);
    }

    internal void LinkComponent()
    {
        number = transform.Find("Number").GetComponent<Text>();
        itembox = GetComponent<ItemBox>();
        itembox.LinkComponent();
    }
}
