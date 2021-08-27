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
        ItemBox fromItemBox = eventData.pointerDrag.GetComponent<ItemBox>();
        int itemUid = fromItemBox.inventoryItemInfo.uid;

        // 존 박스 제거
        SkillUI.Instance.ClearBox(itemUid);

        // 박스에 아이템 표시
        itembox.Init(fromItemBox.inventoryItemInfo);
        UserData.Instance.itemData.data.quickItemUIDs[index] = itemUid;

        // 기존에 퀵 슬롯에 등록 되어 있던게 있었다면 해제 하자. <- 꼭 해야하나? <- 버그라고 느끼는 사람도 있다 하자!
        
    }

    int index;
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
        print(number.text);
        if (itembox.inventoryItemInfo == null)
            return;

        if (endTime > Time.time)
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
        endTime = Time.time + coolTimeSeconds;

        while (endTime > Time.time)
        {
            float remainTime = endTime - Time.time;
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
