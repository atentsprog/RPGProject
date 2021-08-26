using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemBox))]
public class QuickItemUseBox : MonoBehaviour
{
    ItemBox itembox;

    internal void LinkComponent()
    {
        itembox = GetComponent<ItemBox>();
        itembox.LinkComponent();
    }
}
