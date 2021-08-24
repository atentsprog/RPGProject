using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemType
{
    None,
    Money,
    Weapon,     // 1001
    Armor,      // 2001
    Accesory,   // 3001
    Consume,    // 4001
    Material,   // 5001
}

public partial class ShopUI : Singleton<ShopUI>
{
    internal void Show(ShopUI.ShopType currentType)
    {
        switch (currentType)
        {
            case ShopType.Buy:
                InitBuyCategory();
                ShowBuyList(ItemType.Weapon);
                break;
            case ShopType.Sell:
                break;
            case ShopType.Craft:
                break;
        }

        void InitBuyCategory()
        {
            List<Tuple<string, UnityAction>> categoryAction = new List<Tuple<string, UnityAction>>();
            categoryAction.Add(new Tuple<string, UnityAction>("무기"        , () => ShowBuyList(ItemType.Weapon)));
            categoryAction.Add(new Tuple<string, UnityAction>("방어구"      , () => ShowBuyList(ItemType.Armor)));
            categoryAction.Add(new Tuple<string, UnityAction>("악세사리"    , () => ShowBuyList(ItemType.Accesory)));
            categoryAction.Add(new Tuple<string, UnityAction>("소비 아이템" , () => ShowBuyList(ItemType.Consume)));
            categoryAction.Add(new Tuple<string, UnityAction>("재료"        , () => ShowBuyList(ItemType.Material)));

            TextButtonBox menuCategoryBoxBase;
            menuCategoryBoxBase = transform.Find("SubCategory/Left/Content/CotegoryBox")
                .GetComponent<TextButtonBox>();
            menuCategoryBoxBase.LinkComponent();

            menuCategoryBoxBase.gameObject.SetActive(true);
            foreach (var item in categoryAction)
            {
                var newButton = Instantiate(menuCategoryBoxBase, menuCategoryBoxBase.transform.parent);

                newButton.text.text = item.Item1;
                newButton.button.onClick.AddListener(item.Item2);
            }
            menuCategoryBoxBase.gameObject.SetActive(false);
        }
    }

    private void ShowBuyList(ItemType itemType)
    {
        print($"{itemType} 보여주자.");
        List<ItemInfo> visibleItemList = ItemDB.Instance.GetItems(itemType);

        foreach(var item in visibleItemList)
        {
            //new 
        }
    }
}
