using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public partial class ShopUI : Singleton<ShopUI>
{
    Text selectedTitle;
    ShopItemListBox shopItemListBoxBase;
    Text shopType;

    private void InitBuyUI()
    {
        shopType = transform.Find("SubCategory/Left/ShopType").GetComponent<Text>();
        selectedTitle = transform.Find("SubCategory/Right/Title/SelectedTitle/SelectedTitle").GetComponent<Text>();
        shopItemListBoxBase = transform.Find("SubCategory/Right/Scroll View/Viewport/Content/Item").GetComponent<ShopItemListBox>();
        shopItemListBoxBase.LinkComponent();
    }

    private string GetItemTypeString(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Money: return "재화";
            case ItemType.Weapon: return "무기";
            case ItemType.Armor: return "방어구";
            case ItemType.Accesory: return "악세사리";
            case ItemType.Consume: return "소비 아이템";
            case ItemType.Material: return "재료";
            case ItemType.Etc: return "기타";
            default: return "";
        }
    }
    private void ShowSellUI()
    {
        ShowBuyAndSellUI(ShowSellList);
        ShowSellList(ItemType.Weapon);
    }

    private void ShowBuyUI()
    {
        ShowBuyAndSellUI(ShowBuyList);
        ShowBuyList(ItemType.Weapon);
    }
    private void ShowBuyAndSellUI(Action<ItemType> action)
    {
        shopMenuGo.SetActive(false);
        subCategoryGo.SetActive(true);

        // Buy, Sell, Craft, Exit
        InitCategory();

        void InitCategory()
        {
            categoryBaseBox = transform.Find("SubCategory/Left/Content/CotegoryBox")
                .GetComponent<TextButtonBox>();

            //"Buy", ShowBuyUI
            List<Tuple<string, UnityAction>> commandList = new List<Tuple<string, UnityAction>>();
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Weapon), () => action(ItemType.Weapon)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Armor), () => action(ItemType.Armor)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Accesory), () => action(ItemType.Accesory)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Consume), () => action(ItemType.Consume)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Material), () => action(ItemType.Material)));

            categoryButtons.ForEach(x => Destroy(x));
            categoryButtons.Clear();

            categoryBaseBox.LinkComponent();
            categoryBaseBox.gameObject.SetActive(true);
            foreach (var item in commandList)
            {
                var newButton = Instantiate(categoryBaseBox, categoryBaseBox.transform.parent);
                categoryButtons.Add(newButton.gameObject);
                newButton.text.text = item.Item1;
                newButton.button.onClick.AddListener(item.Item2);
            }
            categoryBaseBox.gameObject.SetActive(false);
        }
    }


    List<GameObject> categoryButtons = new List<GameObject>();
    List<GameObject> shopItems = new List<GameObject>();
    private void ShowBuyList(ItemType itemType)
    {
        shopType.text = "Buy";
        selectedTitle.text = GetItemTypeString(itemType);

        // 리스트를 표시하자.
        List<ItemInfo> showItemList = ItemDB.Instance.GetItems(itemType);

        shopItems.ForEach(x => Destroy(x));
        shopItems.Clear();

        shopItemListBoxBase.gameObject.SetActive(true);
        foreach (var item in showItemList)
        {
            ShopItemListBox newBox = Instantiate(shopItemListBoxBase, shopItemListBoxBase.transform.parent);
            newBox.Init(item);
            shopItems.Add(newBox.gameObject);

            newBox.button.onClick.AddListener(() => OnClick(item));
        }
        shopItemListBoxBase.gameObject.SetActive(false);

        void OnClick(ItemInfo item)
        {
            print(item.name);
            SetGuideText($"{item.name}을 구입 하시겠습니까?",
                () => {
                    print($"{item.name}을 구입하자.");

                    string result = UserData.Instance.ProcessBuy(item, 1);
                    SetGuideText(result);
            });
        }
    }
    private void ShowSellList(ItemType itemType)
    {
        shopType.text = "Sell";
        selectedTitle.text = GetItemTypeString(itemType);

        // 리스트를 표시하자.
        List<InventoryItemInfo> showItemList = UserData.Instance.GetItems(itemType);

        shopItems.ForEach(x => Destroy(x));
        shopItems.Clear();

        shopItemListBoxBase.gameObject.SetActive(true);
        foreach (var item in showItemList)
        {
            ShopItemListBox newBox = Instantiate(shopItemListBoxBase, shopItemListBoxBase.transform.parent);
            newBox.Init(item.ItemInfo);
            shopItems.Add(newBox.gameObject);

            newBox.button.onClick.AddListener(() => OnClick(item));
        }
        shopItemListBoxBase.gameObject.SetActive(false);

        void OnClick(InventoryItemInfo item)
        {
            string itemName = item.ItemInfo.name;
            print(itemName);
            SetGuideText($"{itemName}을 판매 하시겠습니까?",
                () => {
                    print($"{itemName}을 판매하자.");

                    string result = UserData.Instance.ProcessSell(item, 1);
                    SetGuideText(result);
                    ShowSellList(itemType);
                });
        }
    }
}
