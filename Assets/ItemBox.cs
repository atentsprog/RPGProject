using System;
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
        if (item != null)
        {
            icon.enabled = true;
            if (item.quickSlotType == QuickSlotType.Item)
            {
                icon.transform.localScale = Vector3.one;
                icon.sprite = item.ItemInfo.Sprite;
                count.text = item.count.ToString();
            }
            else
            {
                //스케일 0.3으로 하자.
                icon.sprite = item.SkillInfo.Sprite;
                icon.transform.localScale = Vector3.one * 0.3f;
                count.text = string.Empty;
            }

            icon.SetNativeSize();
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
            count.text = "";
        }
    }
}
