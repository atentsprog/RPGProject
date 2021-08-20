using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType
{
    None,
    Gold = 1,

    Weapon,
    Armor,
    Acc,
    Consume,
    Etc,
}

[System.Serializable]
public class RewardInfo
{
    public int id;
    public int count;
}

[System.Serializable]
public class ItemInfo
{
    public string itemName;
    public string iconName;
    public ItemType itemType;
    public int id;
    public int sellPrice;
    public int buyPrice;

    public Sprite Sprite  => Resources.Load<Sprite>($"Icons/{iconName}");
}

[System.Serializable]
public class MonsterInfo
{
    public int id;
    public string iconName;
    public string monsterName;
}

[System.Serializable]
public class DestinationInfo
{
    public int id;
    public string destinationName;
}


public enum QuestType
{
    KillMonster,      // ���� óġ.
    GoToDestination,  // ������ ����
    ItemCollection, // ������ ����
}

public class ItemDB : Singleton<ItemDB>
{
    [SerializeField] List<ItemInfo> itemList;
    [SerializeField] Dictionary<int, ItemInfo> itemMap;

    [SerializeField] List<MonsterInfo> monsterList;
    [SerializeField] Dictionary<int, MonsterInfo> monsterMap;

    [SerializeField] List<DestinationInfo> destinationList;
    [SerializeField] Dictionary<int, DestinationInfo> destinationMap;

    private void Awake()
    {
        itemMap = itemList.ToDictionary(x => x.id);
        monsterMap = monsterList.ToDictionary(x => x.id);
        destinationMap = destinationList.ToDictionary(x => x.id);
    }

    internal static ItemInfo GetItemInfo(int itemID)
    {
        if (Instance.itemMap.TryGetValue(itemID, out ItemInfo result) == false)
            Debug.LogError($"������ ID {itemID}�� �����ϴ�");

        return result;
    }

    internal static MonsterInfo GetMosngterInfo(int monsterID)
    {
        if (Instance.monsterMap.TryGetValue(monsterID, out MonsterInfo result) == false)
            Debug.LogError($"���� ID {monsterID}�� �����ϴ�");

        return result;
    }
    internal static DestinationInfo GetDestinationInfo(int destinationID)
    {
        if (Instance.destinationMap.TryGetValue(destinationID, out DestinationInfo result) == false)
            Debug.LogError($"��� ID {destinationID}�� �����ϴ�");

        return result;
    }
}
