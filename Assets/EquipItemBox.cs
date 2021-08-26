using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemBox))]
public class EquipItemBox : MonoBehaviour, IDropHandler
{
    public int index;
    public ItemBox itemBox;

    public void OnDrop(PointerEventData eventData)
    {
        print(eventData);
        ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
        itemBox.Init(fromItemBox.inventoryItemInfo);

        int itemUid = fromItemBox.inventoryItemInfo.uid;
        UserData.Instance.itemData.data.equipItemUIDs[index] = itemUid;
    }

    internal void Init(int _index, InventoryItemInfo inventoryItemInfo)
    {
        index = _index;
        itemBox.Init(inventoryItemInfo);
    }

    internal void LinkComponent()
    {
        itemBox = GetComponent<ItemBox>();
        itemBox.LinkComponent();
    }
}
