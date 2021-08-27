using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : Singleton<InventoryUI>
{
    void Awake()
    {
        List<TextButtonBox> categoryBox = new List<TextButtonBox>();
        for (int i = 1; i <= 6; i++)
        {
            var button = transform.Find($"Left/CategoryBox{i}").GetComponent<TextButtonBox>();
            button.LinkComponent();

            //button.button.onClick.AddListener(() => ShowItemCategory((ItemType)i)); <-- 하면 안됨.
            ItemType itemType = (ItemType)i;
            button.button.onClick.AddListener(() =>ShowItemCategory(itemType)); // 값 복사 해서 사용 해야함(잘했음)

            categoryBox.Add(button);
        }

        baseBox = transform.Find("Middle/Scroll View/Viewport/Content/Item").GetComponent<ItemBox>();
    }

    List<GameObject> inventoryGos = new List<GameObject>();

    ItemBox baseBox;
    private void ShowItemCategory(ItemType itemType)
    {
        gameObject.SetActive(true);

        // 리스트를 표시하자.
        List<InventoryItemInfo> showItemList = UserData.Instance.GetItems(itemType);

        inventoryGos.ForEach(x => Destroy(x));
        inventoryGos.Clear();

        baseBox.gameObject.SetActive(true);
        foreach (var item in showItemList)
        {
            ItemBox newBox = Instantiate(baseBox, baseBox.transform.parent);
            newBox.Init(item, false, () => OnClick(item));
            inventoryGos.Add(newBox.gameObject);
        }
        baseBox.gameObject.SetActive(false);

        void OnClick(InventoryItemInfo item)
        {
            string itemName = item.ItemInfo.name;
            print(itemName);

            ItemInfoUI.Instance.ShowUI(item);
        }
    }
    private void OnEnable()
    {
        StageManager.GameState = GameStateType.Menu;
    }
    private void OnDisable()
    {
        StageManager.GameState = GameStateType.Play;
    }

    public void ShowUI()
    {
        ShowItemCategory(ItemType.Weapon);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
