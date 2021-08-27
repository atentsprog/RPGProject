using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : Singleton<ItemInfoUI>
{
    Text title;
    Text explain;
    ItemBox itemBox;
    Button baseButton;
    private void Awake()
    {
        baseButton = transform.Find("Buttons/Button").GetComponent<Button>();
        itemBox = GetComponentInChildren<ItemBox>();
        title = transform.Find("Title").GetComponent<Text>();
        explain = transform.Find("Explain").GetComponent<Text>();

        baseButton.gameObject.SetActive(false);
        itemBox.LinkComponent();
        itemBox.Init(null);
        title.text = "";
        explain.text = "";
    }

    internal void ShowUI(InventoryItemInfo item)
    {
        ItemInfo itemInfo = ItemDB.GetItemInfo(item.id);
        title.text = itemInfo.name;
        explain.text = itemInfo.description;
        itemBox.Init(item);
    }
}
