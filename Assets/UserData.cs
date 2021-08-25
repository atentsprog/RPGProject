using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UserQuestData
{
    public List<int> acceptIds = new List<int>();
    public List<int> rejectIds = new List<int>();
}

[System.Serializable]
public class UserItemData
{
    public int lastUID;
    public List<InventoryItemInfo> item = new List<InventoryItemInfo>();
}

[Serializable]
public class PlayerData
{
    public int gold;
}
public class UserData : Singleton<UserData>
{
    public PlayerPrefsData<UserQuestData> questData;
    public PlayerPrefsData<UserItemData> itemData;
    public PlayerPrefsData<PlayerData> playerData;
    internal Action<int, int> changedGold;

    //내가 구입한 아이템.

    private void Awake()
    {
        questData = new PlayerPrefsData<UserQuestData>("UserQuestData");
        itemData = new PlayerPrefsData<UserItemData>("UserItemData");
        playerData = new PlayerPrefsData<PlayerData>("PlayerData");
    }

    private void OnDestroy()
    {
        questData.SaveData();
        itemData.SaveData();
        playerData.SaveData();
    }

    private void AddItem(ItemInfo item)
    {
        var existItem = itemData.data.item
            .Where(x => x.id == item.id && x.count < item.maxStackCount)
            .FirstOrDefault();
        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            InventoryItemInfo newItem = new InventoryItemInfo();
            newItem.id = item.id;
            newItem.count = 1;
            newItem.uid = ++itemData.data.lastUID;
            itemData.data.item.Add(newItem);
        }
    }

    private void RemoveItem(InventoryItemInfo item)
    {
        itemData.data.item.Remove(item);
    }

    private bool IsEnoughMoney(int needMoney)
    {
        return playerData.data.gold >= needMoney;
    }

    private void AddGold(int gold)
    {
        int oldValue = playerData.data.gold;
        playerData.data.gold += gold;
        int newValue = playerData.data.gold;

        changedGold.Invoke(oldValue, newValue);
    }

    private void SubGold(int gold)
    {
        int oldValue = playerData.data.gold;
        playerData.data.gold -= gold;
        int newValue = playerData.data.gold;

        changedGold.Invoke(oldValue, newValue);
    }

    internal List<InventoryItemInfo> GetItems(ItemType itemType)
    {
        return itemData.data.item
            .Where(x => x.ItemInfo.itemType == itemType)
            .ToList();
    }

    internal string ProcessBuyItem(ItemInfo item, int count)
    {
        int totalPrice = item.buyPrice * count;
        //돈이 있는지 확인하자.
        if (IsEnoughMoney(totalPrice) == false)
            return "돈이 충분하지 않습니다.";

        for (int i = 0; i < count; i++)
            AddItem(item);

        SubGold(totalPrice); // sub = Subtract

        return $"{item.name} 구입 했습니다.";
    }

    internal string ProcessSellItem(InventoryItemInfo item, int count)
    {
        int totalPrice = item.ItemInfo.sellPrice * count;

        for (int i = 0; i < count; i++)
            RemoveItem(item);

        AddGold(totalPrice); // sub = Subtract

        return $"{item.ItemInfo.name} 판매 했습니다.";
    }
}
