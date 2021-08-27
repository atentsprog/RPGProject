using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBox : MonoBehaviour
{
    public Image icon;
    public Button button;
    public Text count;
    public GameObject activeGo;

    private void Awake()
    {
        LinkComponent();
    }
    bool completeLink;
    public void LinkComponent()
    {
        if (completeLink)
            return;
        completeLink = true;

        if (icon == null)
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
    internal void Init(InventoryItemInfo item, bool activeState = false, UnityAction action = null)
    {
        inventoryItemInfo = item;
        if (item != null)
        {
            icon.enabled = true;
            icon.sprite = item.ItemInfo.Sprite;
            count.text = item.count.ToString();
        }
        else
        {
            icon.enabled = false;
            count.text = "";
        }

        if (activeGo != null)
            activeGo.SetActive(activeState);

        button.onClick.RemoveAllListeners();
        if(action != null)
            button.onClick.AddListener(action);
    }
}
