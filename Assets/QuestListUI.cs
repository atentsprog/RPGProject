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
    QuestTitleBox baseQuestTitleBox;
    RewardBox baseRewardBox;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        baseQuestTitleBox = GetComponentInChildren<QuestTitleBox>();
        baseRewardBox = GetComponentInChildren<RewardBox>();
        baseQuestTitleBox.Init();
        baseRewardBox.Init();
    }

    public void ShowQuestList()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        // 왼쪽에 있는 퀘스트 이름 리스트 초기화
        baseQuestTitleBox.gameObject.SetActive(true);
        foreach (var item in quests)
        {
            var titleItem = Instantiate(baseQuestTitleBox, baseQuestTitleBox.transform);
            titleItem.Init(item);
        }
        baseQuestTitleBox.gameObject.SetActive(false);
    }
}
