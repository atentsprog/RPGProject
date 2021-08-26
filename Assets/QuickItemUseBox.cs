using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemBox))]
public class QuickItemUseBox : MonoBehaviour, IDropHandler
{
    ItemBox itembox;

    public void OnDrop(PointerEventData eventData)
    {
        print(eventData);
    }

    internal void LinkComponent()
    {
        itembox = GetComponent<ItemBox>();
        itembox.LinkComponent();
    }
}
