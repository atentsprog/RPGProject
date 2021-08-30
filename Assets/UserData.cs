﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UserQuestData
{
    public List<int> acceptIds = new List<int>();
    public List<int> rejectIds = new List<int>();
    public int activeQuestID;
}

[System.Serializable]
public class UserItemData
{
    public int lastUID;
    public List<InventoryItemInfo> item = new List<InventoryItemInfo>();
    public List<int> quickItemUIDs = new List<int>();
    public List<int> equipItemUIDs = new List<int>();
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
    internal Action<int, int> onChangedGold;

    //내가 구입한 아이템.

    private void Awake()
    {
        questData = new PlayerPrefsData<UserQuestData>("UserQuestData");
        itemData = new PlayerPrefsData<UserItemData>("UserItemData");
        accountData = new PlayerPrefsData<AccountData>("AccountData");
        if (itemData.data.quickItemUIDs.Count == 0)
            itemData.data.quickItemUIDs.AddRange(new int[10]);
        if (itemData.data.equipItemUIDs.Count == 0)
            itemData.data.equipItemUIDs.AddRange(new int[8]);


        onChangedGold?.Invoke(0, accountData.data.gold);
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
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
            QuickSlotUI.Instance.UpdateItemInfo(existItem);
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
        int oldValue = accountData.data.gold;
        accountData.data.gold -= subGold;
        onChangedGold?.Invoke(oldValue, accountData.data.gold);
    }
    private void AddGold(int subGold)
    {
        int oldValue = accountData.data.gold;
        accountData.data.gold += subGold; 
        onChangedGold?.Invoke(oldValue, accountData.data.gold);
    }

    private bool IsEnoughGold(int needGold)
    {
        return accountData.data.gold >= needGold;
    }

    internal List<InventoryItemInfo> GetItems(ItemType itemType)
    {
        return Instance.itemData.data.item
            .Where(x => x.ItemInfo.itemType == itemType)
            .ToList();
    }

    internal string ProcessSell(InventoryItemInfo item, int count)
    {
        int totalGold = item.ItemInfo.sellPrice * count;
        // 아이템 삭제
        RemoveItem(item, count);

        // 돈 추가
        AddGold(totalGold);

        // 퀵슬롯 UI에서 삭제.
        QuickSlotUI.Instance.ClearSlot(item.uid);
        // <- 이로직 실행해도 껐다 켜면 저장안되어 있음
        // ClearSlot 실행시 퀵슬롯 정보 저장하게 수정해야함

        return $"{item.ItemInfo.name} 판매 했습니다";
    }

    private void RemoveItem(InventoryItemInfo item, int deleteCount)
    {
        item.count -= deleteCount;
        Debug.Assert(item.count >= 0, "0보다 작아질 수 없어");
        if (item.count == 0)
            itemData.data.item.Remove(item);
    }

    internal InventoryItemInfo GetItem(int itemUID)
    {
        return itemData.data.item.Where(x => x.uid == itemUID)
            .FirstOrDefault();
    }
}
