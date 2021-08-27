using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : Singleton<ItemInfoUI>
{
    ItemBox itemBox;
    Text title;
    Text description;

    void Awake()
    {
        itemBox = GetComponentInChildren<ItemBox>();
        title = transform.Find("Title").GetComponent<Text>();
        description = transform.Find("Description").GetComponent<Text>();
        itemBox.LinkComponent();
    }

    public void ShowUI(InventoryItemInfo item)
    {
        title.text = item.ItemInfo.name;
        description.text = item.ItemInfo.description;
        itemBox.Init(item);
    }
}
