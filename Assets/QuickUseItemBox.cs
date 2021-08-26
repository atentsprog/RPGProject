using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ItemBox))]
public class QuickUseItemBox : MonoBehaviour, IDropHandler
{
    ItemBox itemBox;
    Text shortcutNumber;

    Text coolTimeText;
    Image coolTimeBG;

    [SerializeField] InputAction inputAction;

    private void Awake()
    {
        shortcutNumber = transform.Find("Number").GetComponent<Text>();
        coolTimeText = transform.Find("CoolTimeText").GetComponent<Text>();
        coolTimeBG   = transform.Find("CoolTimeBG").GetComponent<Image>();

        itemBox = GetComponent<ItemBox>();
        itemBox.LinkComponent();
        itemBox.button.onClick.AddListener(() => UseSkill());
    }

    float endCoolTime;
    public float coolTime = 3;
    private void UseSkill()
    {
        print("UseSkill " + shortcutNumber.text);
        if(itemBox.inventoryItemInfo!= null)
            print(itemBox.inventoryItemInfo.ItemInfo.name);

        if (endCoolTime > Time.time)
            return;
        StartCoroutine(CoolTimeCo());
    }

    public float punchTime = 0.5f;
    private IEnumerator CoolTimeCo()
    {
        endCoolTime = Time.time + coolTime;

        while (endCoolTime > Time.time)
        {
            float remainTime = endCoolTime - Time.time;

            //coolTimeText.text = ((int)(remainTime + 1)).ToString();
            //coolTimeText.text = (remainTime + 1).ToString("0.0");
            coolTimeText.text = remainTime.ToString("0.0");

            float remainPercent = remainTime / coolTime;   // 1.0 -> 0.0
            coolTimeBG.fillAmount = 1 - remainPercent;       // 0.0 -> 1.0

            yield return null;
        }
        coolTimeText.text = "";
        coolTimeBG.fillAmount = 1;
        coolTimeBG.transform.DOPunchScale(coolTimeBG.transform.localScale, punchTime);
    }

    public int index;
    public void Init(int _index, string bindingString)
    {
        index = _index;
        inputAction = new InputAction("Key", InputActionType.Button, bindingString);
        inputAction.performed += InputAction_performed;
        inputAction.Enable();

        shortcutNumber.text = bindingString.Replace("<Keyboard>/", "");
    }

    private void InputAction_performed(InputAction.CallbackContext obj)
    {
        UseSkill();
    }

    public void SetItem(InventoryItemInfo inventoryItemInfo)
    {
        itemBox.Init(inventoryItemInfo);
    }

    public void OnDrop(PointerEventData data)
    {
        ItemBox fromItemBox = data.pointerDrag.GetComponent<ItemBox>();

        SetItem(fromItemBox.inventoryItemInfo);
        int uid = 0;
        if (fromItemBox.inventoryItemInfo != null)
            uid = fromItemBox.inventoryItemInfo.uid;
        UserData.Instance.itemData.data.shortcutItemUIDs[index] = uid;
    }
}
