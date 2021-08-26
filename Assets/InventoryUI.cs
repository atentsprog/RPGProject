using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : Singleton<InventoryUI>
{
    CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        List<TextButtonBox> categoryBox = new List<TextButtonBox>();
        for (int i = 1; i <= 6; i++)
        {
            var button = transform.Find($"Left/CategoryBox{i}").GetComponent<TextButtonBox>();
            button.LinkComponent();
            categoryBox.Add(button);
        }

        categoryBox[0].button.onClick.AddListener(() => ShowItemCategory(ItemType.Weapon));
        categoryBox[1].button.onClick.AddListener(() => ShowItemCategory(ItemType.Armor));
        categoryBox[2].button.onClick.AddListener(() => ShowItemCategory(ItemType.Accesory));
        categoryBox[3].button.onClick.AddListener(() => ShowItemCategory(ItemType.Consume));
        categoryBox[4].button.onClick.AddListener(() => ShowItemCategory(ItemType.Material));
        categoryBox[5].button.onClick.AddListener(() => ShowItemCategory(ItemType.Etc));
     
        baseBox = transform.Find("Middle/Scroll View/Viewport/Content/Item").GetComponent<ItemBox>();
    }

    private void OnEnable()
    {
        StageManager.GameState = GameStateType.Menu;
    }
    private void OnDisable()
    {
        StageManager.GameState = GameStateType.Play;
    }

    List<GameObject> inventoryGos = new List<GameObject>();

    ItemBox baseBox;
    private void ShowItemCategory(ItemType itemType)
    {
        gameObject.SetActive(true);
        UiUtil.FadeShowUI(canvasGroup);

        // 리스트를 표시하자.
        List<InventoryItemInfo> showItemList = UserData.Instance.GetItems(itemType);

        inventoryGos.ForEach(x => Destroy(x));
        inventoryGos.Clear();

        baseBox.gameObject.SetActive(true);
        foreach (var item in showItemList)
        {
            ItemBox newBox = Instantiate(baseBox, baseBox.transform.parent);
            newBox.Init(item, true);
            inventoryGos.Add(newBox.gameObject);

            newBox.button.onClick.AddListener(() => OnClick(item));
        }
        baseBox.gameObject.SetActive(false);

        void OnClick(InventoryItemInfo item)
        {
            string itemName = item.ItemInfo.name;
            print(itemName);
            //SetGuideText($"{itemName}을 판매 하시겠습니까?",
            //    () => {
            //        print($"{itemName}을 판매하자.");

            //        string result = UserData.Instance.ProcessSell(item, 1);
            //        SetGuideText(result);
            //        ShowSellList(itemType);
            //    });
        }
    }

    public void ShowUI()
    {
        ShowItemCategory(ItemType.Weapon);
    }

    public void CloseUI()
    {
        UiUtil.FadeCloseUI(canvasGroup);
    }
}

public static class UiUtil
{
    public static void FadeCloseUI(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                canvasGroup.gameObject.SetActive(false);
            });
    }

    public static void FadeShowUI(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);
    }
}
