using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        List<TextButtonBox> categoryBox = new List<TextButtonBox>();
        for (int i = 1; i <= 6; i++)
        {
            categoryBox.Add(transform.Find($"Left/CategoryBox{i}").GetComponent<TextButtonBox>());
        }

        categoryBox[0].button.onClick.AddListener(()=>ShowItemCategory(ItemType.Weapon));
        categoryBox[1].button.onClick.AddListener(()=>ShowItemCategory(ItemType.Armor));
        categoryBox[2].button.onClick.AddListener(()=>ShowItemCategory(ItemType.Accesory));
        categoryBox[3].button.onClick.AddListener(()=>ShowItemCategory(ItemType.Consume));
        categoryBox[4].button.onClick.AddListener(()=>ShowItemCategory(ItemType.Material));
        categoryBox[5].button.onClick.AddListener(()=>ShowItemCategory(ItemType.Etc));
    }

    List<GameObject> inventoryGos = new List<GameObject>();

    TextButtonBox shopItemListBoxBase;
    private void ShowItemCategory(ItemType itemType)
    {

        // 리스트를 표시하자.
        List<InventoryItemInfo> showItemList = UserData.Instance.GetItems(itemType);

        inventoryGos.ForEach(x => Destroy(x));
        inventoryGos.Clear();

        shopItemListBoxBase.gameObject.SetActive(true);
        foreach (var item in showItemList)
        {
            ShopItemListBox newBox = Instantiate(shopItemListBoxBase, shopItemListBoxBase.transform.parent);
            newBox.Init(item.ItemInfo);
            inventoryGos.Add(newBox.gameObject);

            newBox.button.onClick.AddListener(() => OnClick(item));
        }
        shopItemListBoxBase.gameObject.SetActive(false);

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
