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

[System.Serializable]
public class AccountData
{
    public int gold;
    public int crystal;
    public string userName;
}


public class UserData : Singleton<UserData>
{
    public PlayerPrefsData<UserQuestData> questData;
    public PlayerPrefsData<UserItemData> itemData;
    public PlayerPrefsData<AccountData> accountData;

    //내가 구입한 아이템.

    private void Awake()
    {
        questData = new PlayerPrefsData<UserQuestData>("UserQuestData");
        itemData = new PlayerPrefsData<UserItemData>("UserItemData");
        accountData = new PlayerPrefsData<AccountData>("AccountData");
    }

    private void OnDestroy()
    {
        questData.SaveData();
        itemData.SaveData();
        accountData.SaveData();
    }

    internal string ProcessBuy(ItemInfo item, int count)
    {
        int totalGold = item.buyPrice * count;
        // 돈이 있는지 확인.
        if (IsEnoughGold(totalGold) == false)
            return "돈이 충분하지 않습니다";

        // 아이템 지급
        for (int i = 0; i < count; i++)
        {
            InsertItem(item);
        }

        // 돈 빼기
        SubGold(totalGold);

        return $"{item.name} 구입 했습니다";
    }

    private void InsertItem(ItemInfo buyItem)
    {
        var existItem = itemData.data.item
            .Where(x => x.id == buyItem.id && x.count < buyItem.maxStackCount)
            .FirstOrDefault();

        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            InventoryItemInfo newItem = new InventoryItemInfo();
            newItem.id = buyItem.id;
            newItem.count = 1;
            newItem.uid = ++itemData.data.lastUID;
            itemData.data.item.Add(newItem);
        }
    }

    private void SubGold(int subGold)
    {
        accountData.data.gold -= subGold;
    }
    private void AddGold(int subGold)
    {
        accountData.data.gold += subGold;
    }

    private bool IsEnoughGold(int needGold)
    {
        return accountData.data.gold >= needGold;
    }
}
