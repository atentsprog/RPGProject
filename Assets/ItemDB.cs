using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestType
{
    KillMonster,      // 몬스터 처치.
    GoToDestination,  // 목적지 도착
    ItemCollection, // 아이템 수집
}
[System.Serializable]
public class RewardInfo
{
    public int itemID;
    public int count;
}

[System.Serializable]
public class QuestInfo
{
    public string questTitle;
    public int id;
    [TextArea]
    public string detailExplain;
    public QuestType questType;

    /// <summary>
    /// 몬스터 처치시는 몬스터 ID
    /// 아이템 수집시는 아이템 ID,</summary>
    public int goalId;
    /// <summary>
    /// 몬스터 처치시는 몬스터 처치수
    /// 아이템 수집시는 아이템 수집수,</summary>
    public int goalCount;

    public List<RewardInfo> rewards;

    internal string GetGoalString()
    {
        switch (questType)
        {
            case QuestType.KillMonster: // 슬라임을 5마리 처치하세요.
                string monsterName = ItemDB.GetMosnterInfo(goalId).name;
                return $"{monsterName}를 {goalCount}마리 잡으세요";
            case QuestType.GoToDestination: // 촌장님댁으로 이동하세요.
                string destinationName = ItemDB.GetDestinationInfo(goalId).name;
                return $"{destinationName}에 가세요";
            case QuestType.ItemCollection: // 보석을 5개 수집하세요
                string itemName = ItemDB.GetItemInfo(goalId).name;
                return $"{itemName}를 {goalCount}개 수집하세요";
        }

        return "임시 작업해야함";
    }
}

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
public class SkillInfo
{
    public string name;
    public int id;
    public int mana;
    [TextArea]
    public string description;
    public string icon;
    public Sprite Sprite => Resources.Load<Sprite>($"Icons/{icon}");

    internal InventoryItemInfo GetInventoryItemInfo()
    {
        InventoryItemInfo inventoryItemInfo = new InventoryItemInfo();
        inventoryItemInfo.id = inventoryItemInfo.uid = id;
        inventoryItemInfo.type = QuickSlotType.Skill;
        return inventoryItemInfo;
    }
}


[System.Serializable]
public class ItemInfo
{
    public string name;
    public int id;
    [TextArea]
    public string description;
    public string iconName;
    public Sprite Sprite => Resources.Load<Sprite>($"Icons/{iconName}");

    public int sellPrice; // 상점에 팔때
    public int buyPrice; // 상점에서 구입할때 가격
    public bool registShop;
    public ItemType itemType;

    public int maxStackCount; // 1개까지 스택으로 쌓임.
}


public enum ItemType
{
    Money,      // 재화. 0 ~ 10
    Weapon,     // 무기       1001
    Armor,      // 방어구     2001 ~ 3000
    Accesory,   // 악세사리.  3001
    Consume,    // 소비아이템(포션,..) // 4001
    Material,   // 재료       5001
    Etc,        // 기타       6001
}


public class ItemDB : Singleton<ItemDB>
{
    [SerializeField] List<QuestInfo> quests;
    [SerializeField] List<ItemInfo> items;
    [SerializeField] List<MonsterInfo> monsters;
    [SerializeField] List<DestinationInfo> destinations;
    public List<SkillInfo> skills;
    Dictionary<int, QuestInfo> questMap;
    Dictionary<int, ItemInfo> itemMap;
    Dictionary<int, MonsterInfo> monsterMap;
    Dictionary<int, DestinationInfo> destinationMap;
    Dictionary<int, SkillInfo> skillMap;

    private void Awake()
    {
        itemMap = items.ToDictionary(x => x.id);
        monsterMap = monsters.ToDictionary(x => x.id);
        destinationMap = destinations.ToDictionary(x => x.id);
        questMap = quests.ToDictionary(x => x.id);
        skillMap = skills.ToDictionary(x => x.id);
    }

    internal List<ItemInfo> GetItems(ItemType itemType)
    {
        return items.Where(x => x.itemType == itemType).ToList();
    }

    internal List<QuestInfo> GetQuestInfo(List<int> questIds)
    {
        List<QuestInfo> result = new List<QuestInfo>(questIds.Count);

        foreach (var item in questIds)
        {
            result.Add(GetQuestInfo(item));
        }
        return result;
        //return quests.Where(x => questIds.Contains(x.id)).ToList();
    }

    internal static QuestInfo GetQuestInfo(int questID)
    {
        if (Instance.questMap.TryGetValue(questID, out QuestInfo result) == false)
            Debug.LogError($"{questID}가 없습니다");
        return result;
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
            Debug.LogError($"Item ID {itemID}가 없습니다");
        return result;
    }

    internal static DestinationInfo GetDestinationInfo(int destinationId)
    {
        if (Instance.destinationMap.TryGetValue(destinationId, out DestinationInfo result) == false)
            Debug.LogError($"{destinationId}가 없습니다");
        return result;
    }

    internal static SkillInfo GetSkillInfo(int id)
    {
        if (Instance.skillMap.TryGetValue(id, out SkillInfo result) == false)
            Debug.LogError($"Skill ID {id}가 없습니다");
        return result;
    }
}
