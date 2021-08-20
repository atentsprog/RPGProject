using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
퀘스트 이름 : questTitle
상세내용 : detailExplain
목표 Type : enum QuestType
 KillMonster,      // 몬스터 처치.
 GoToDestination,  // 목적지 도착
 ItemCollection, // 아이템 수집
 
GoalCount : 
	몬스터 처치시는 몬스터 처치수
	아이템 수집시는 아이템 수집수,
	
class Reward
{
ItemID
Count 
}*/

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
    [TextArea]
    public string detailExplain;
    public QuestType questType;

    /// <summary>
    /// 몬스터 처치시는 몬스터 ID
    /// 아이템 수집시는 아이템 ID,</summary>
    public int GoalId;
    /// <summary>
    /// 몬스터 처치시는 몬스터 처치수
    /// 아이템 수집시는 아이템 수집수,</summary>
    public int GoalCount;

    public List<RewardInfo> rewards;
}
public class QuestListUI : Singleton<QuestListUI>
{
    CanvasGroup canvasGroup;
    public List<QuestInfo> quests;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void ShowQuestList()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);
    }
}
