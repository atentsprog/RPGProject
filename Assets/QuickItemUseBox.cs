using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemBox))]
public class QuickItemUseBox : MonoBehaviour, IDropHandler
{
    public ItemBox itembox;

    public void OnDrop(PointerEventData eventData)
    {
        print(eventData);
        ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
        itembox.Init(fromItemBox.inventoryItemInfo);
    }

    internal void LinkComponent()
    {
        itembox = GetComponent<ItemBox>();
        itembox.LinkComponent();
    }
}
