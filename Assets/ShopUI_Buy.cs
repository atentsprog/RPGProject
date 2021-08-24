using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public partial class ShopUI : Singleton<ShopUI>
{
    Text selectedTitle;
    private void InitBuyUI()
    {
        selectedTitle = transform.Find("Right/Title/SelectedTitle/SelectedTitle").GetComponent<Text>();
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

    private void ShowBuyUI()
    {
        shopMenuGo.SetActive(false);
        subCategoryGo.SetActive(true);

        // Buy, Sell, Craft, Exit
        InitCategory();

        ShowBuyList(ItemType.Weapon);

        void InitCategory()
        {
            categoryBaseBox = transform.Find("SubCategory/Left/Content/CotegoryBox")
                .GetComponent<TextButtonBox>();

            //"Buy", ShowBuyUI
            List<Tuple<string, UnityAction>> commandList = new List<Tuple<string, UnityAction>>();
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Weapon), () => ShowBuyList(ItemType.Weapon)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Armor), () => ShowBuyList(ItemType.Armor)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Accesory), () => ShowBuyList(ItemType.Accesory)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Consume), () => ShowBuyList(ItemType.Consume)));
            commandList.Add(new Tuple<string, UnityAction>(GetItemTypeString(ItemType.Material), () => ShowBuyList(ItemType.Material)));

            //buyBaseBoxs.ForEach(x => Destroy(x));
            //buyBaseBoxs.Clear();

            categoryBaseBox.LinkComponent();
            categoryBaseBox.gameObject.SetActive(true);
            foreach (var item in commandList)
            {
                var newButton = Instantiate(categoryBaseBox, categoryBaseBox.transform.parent);

                newButton.text.text = item.Item1;
                newButton.button.onClick.AddListener(item.Item2);
                //buyBaseBoxs.Add(newButton.gameObject);
            }
            categoryBaseBox.gameObject.SetActive(false);
        }
    }
    private void ShowBuyList(ItemType itemType)
    {
        selectedTitle.text = GetItemTypeString(itemType);

        // 리스트를 표시하자.
    }
}