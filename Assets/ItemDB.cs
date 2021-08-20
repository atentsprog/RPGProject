using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MonsterInfo
{
    public string name;
    public int id;
}
[System.Serializable]
public class DestinationInfo
{
    public string name;
    public int id;
}
[System.Serializable]
public class ItemInfo
{
    public string name;
    public int id;
}


public class ItemDB : Singleton<ItemDB>
{
    [SerializeField] List<ItemInfo> items;
    [SerializeField] List<MonsterInfo> monsters;
    [SerializeField] List<DestinationInfo> destinations;
    Dictionary<int, ItemInfo> itemMap;
    Dictionary<int, MonsterInfo> monsterMap;
    Dictionary<int, DestinationInfo> destinationMap;
    private void Awake()
    {
        itemMap = items.ToDictionary(x => x.id);
        monsterMap = monsters.ToDictionary(x => x.id);
        destinationMap = destinations.ToDictionary(x => x.id);
    }

    internal static MonsterInfo GetMosnterInfo(int mosnterID)
    {
        if (Instance.monsterMap.TryGetValue(mosnterID, out MonsterInfo result) == false)
            Debug.LogError($"{mosnterID}가 없습니다");
        return result;
    }

    internal static ItemInfo GetItemInfo(int itemID)
    {
        if (Instance.itemMap.TryGetValue(itemID, out ItemInfo result) == false)
            Debug.LogError($"{itemID}가 없습니다");
        return result;
    }

    internal static DestinationInfo GetDestinationInfo(int destinationId)
    {
        if (Instance.destinationMap.TryGetValue(destinationId, out DestinationInfo result) == false)
            Debug.LogError($"{destinationId}가 없습니다");
        return result;
    }
}
