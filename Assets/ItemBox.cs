﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBox : MonoBehaviour
{
    public Image icon;
    public Button button;
    public Text count;
    public GameObject activeGo;
    public void LinkComponent()
    {
        if(icon == null)
            icon = transform.Find("Icon").GetComponent<Image>();
        if (icon == null)
            icon = GetComponentInChildren<Image>();

        if (button == null)
            button = GetComponent<Button>();

        if (count == null)
            count = GetComponentInChildren<Text>();

        if (activeGo == null)
            activeGo = transform.Find("ActiveState")?.gameObject;
    }

    public InventoryItemInfo inventoryItemInfo;
    internal void Init(InventoryItemInfo item)
    {
        inventoryItemInfo = item;
        icon.sprite = item.ItemInfo.Sprite;
        count.text = item.count.ToString();
    }
}