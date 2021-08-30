using DG.Tweening;
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
    public Text coolTimeText;
    public Image coolTimeFilled;

    public ItemBox itembox;
    public Text number;
    public void OnDrop(PointerEventData eventData)
    {
        print(eventData);
        QuickItemUseBox fromQuickItemUseBox = eventData.pointerDrag.GetComponent<QuickItemUseBox>();
        bool fromQuickSlot = fromQuickItemUseBox != null;
        if (fromQuickSlot)
        {
            InventoryItemInfo existInventoryItemInfo = null;
            InventoryItemInfo fromInventoryItemInfo = fromQuickItemUseBox.itembox.inventoryItemInfo;
            // 드랍된곳에 아이템이 이미 있었다면 정보를 바꾸자.
            if (itembox.inventoryItemInfo != null)
                existInventoryItemInfo = itembox.inventoryItemInfo;

            itembox.Init(fromQuickItemUseBox.itembox.inventoryItemInfo);
            UserData.Instance.itemData.data.quickItemUIDs[fromQuickItemUseBox.index] = existInventoryItemInfo.uid;

            fromQuickItemUseBox.itembox.Init(existInventoryItemInfo);
            UserData.Instance.itemData.data.quickItemUIDs[index] = fromInventoryItemInfo.uid;
        }
        else
        {
            ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
            int itemUid = fromItemBox.inventoryItemInfo.uid;

            // 기존에 같은 uid가 들어가 있었으면 해제하자.
            QuickSlotUI.Instance.ClearSlot(itemUid);

            itembox.Init(fromItemBox.inventoryItemInfo);
            UserData.Instance.itemData.data.quickItemUIDs[index] = itemUid;
        }
    }

    public int index;
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
        //print(number.text);
        if (itembox.inventoryItemInfo == null)
            return;

        if (endTime > Time.realtimeSinceStartup)
            return;

        StartCoroutine(StartCoolTimeCo());
    }

    internal void LinkComponent()
    {
        number = transform.Find("Number").GetComponent<Text>();
        itembox = GetComponent<ItemBox>();
        itembox.LinkComponent();
        coolTimeText = transform.Find("CoolTimeText").GetComponent<Text>();
        coolTimeFilled = transform.Find("CoolTimeFilled").GetComponent<Image>();
    }


    float endTime;
    private IEnumerator StartCoolTimeCo()
    {
        float coolTimeSeconds = 3; 
        endTime = Time.realtimeSinceStartup + coolTimeSeconds;

        while (endTime > Time.realtimeSinceStartup)
        {
            float remainTime = endTime - Time.realtimeSinceStartup;
            //remainTimeText1.text = ((int)(remainTime + 1)).ToString();
            //remainTimeText2.text = (remainTime + 1).ToString("0.0");
            coolTimeText.text = remainTime.ToString("0.0");
            float remainPercent = remainTime / coolTimeSeconds;   // 1.0 -> 0.0
            coolTimeFilled.fillAmount = remainPercent;       // 1 -> 1
            yield return null;
        }
        coolTimeText.text = "";
        coolTimeFilled.fillAmount = 0;

        transform.DOPunchScale(Vector3.one * 0.15f, 0.5f);
    }
}
