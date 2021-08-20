using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;

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
public class QuestListUI : Singleton<QuestListUI>
{
    CanvasGroup canvasGroup;
    public List<QuestInfo> quests;
    QuestTitleBox baseQuestTitleBox;
    RewardBox baseRewardBox;
    List<GameObject> questTitleBoxs = new List<GameObject>();
    List<GameObject> rewardBoxs = new List<GameObject>();

    Text selectedQuestTitle;
    Text goalExplain;
    Text questExplain; 
    
    void Start()
    {
        selectedQuestTitle = transform.Find("Right/QuestTitle/SelectedQuestTitle/SelectedQuestTitle").GetComponent<Text>();
        goalExplain = transform.Find("Right/Goal/GoalExplain").GetComponent<Text>();
        questExplain = transform.Find("Right/QuestExplain").GetComponent<Text>();


        transform.Find("GameObject/Yes").GetComponent<Button>().onClick
                .AddListener(() => AcceptQuest());
        transform.Find("GameObject/No").GetComponent<Button>().onClick
                .AddListener(() => RejectQuest());
        transform.Find("CloseButton/Icon").GetComponent<Button>().onClick
                .AddListener(() => CloseUI());

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        baseQuestTitleBox = GetComponentInChildren<QuestTitleBox>();
        baseRewardBox = GetComponentInChildren<RewardBox>();
        baseQuestTitleBox.LinkComponent();
        baseRewardBox.LinkComponent();
    }

    private void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f);
    }

    private void RejectQuest()
    {
        print($"{currentQuest.questTitle} 퀘스트 거절함");
        UserData.Instance.questData.data.rejectIds
            .Add(currentQuest.id);

        ShowQuestList();
    }

    private void AcceptQuest()
    {
        print($"{currentQuest.questTitle} 퀘스트 수락함");
        UserData.Instance.questData.data.acceptIds
            .Add(currentQuest.id);

        ShowQuestList();
    }

    public void ShowQuestList()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        questTitleBoxs.ForEach(x => Destroy(x));
        questTitleBoxs.Clear();
        // 왼쪽에 있는 퀘스트 이름 리스트 초기화
        baseQuestTitleBox.gameObject.SetActive(true);

        List<int> exceptIds = new List<int>();
        exceptIds.AddRange(UserData.Instance.questData.data.acceptIds);
        exceptIds.AddRange(UserData.Instance.questData.data.rejectIds);
        var useQuestList = quests.Where(x => exceptIds.Contains(x.id) == false).ToList();

        foreach (var item in useQuestList)
        {
            var titleItem = Instantiate(baseQuestTitleBox, baseQuestTitleBox.transform.parent);
            titleItem.Init(item);
            titleItem.GetComponent<Button>().onClick
                .AddListener(() => OnClickTitleItem(item));
            questTitleBoxs.Add(titleItem.gameObject);
        }
        baseQuestTitleBox.gameObject.SetActive(false);

        if (useQuestList.Count > 0)
            OnClickTitleItem(useQuestList[0]);
        else
            ClearUI();
    }

    private void ClearUI()
    {
        currentQuest = null;
        selectedQuestTitle.text ="";
        questExplain.text = string.Empty;
        goalExplain.text = string.Empty;


        // 보상리스트 초기화.
        rewardBoxs.ForEach(x => Destroy(x));
        rewardBoxs.Clear();

        questTitleBoxs.ForEach(x => Destroy(x));
        questTitleBoxs.Clear();
    }

    QuestInfo currentQuest;
    private void OnClickTitleItem(QuestInfo item)
    {
        currentQuest = item;
        selectedQuestTitle.text = item.questTitle;
        questExplain.text = item.detailExplain;
        goalExplain.text = item.GetGoalString();


        // 보상리스트 초기화.
        rewardBoxs.ForEach(x => Destroy(x));
        rewardBoxs.Clear();
        baseRewardBox.gameObject.SetActive(true);
        foreach (var rewardItem in item.rewards)
        {
            var titleItem = Instantiate(baseRewardBox, baseRewardBox.transform.parent);
            titleItem.Init(rewardItem);
            rewardBoxs.Add(titleItem.gameObject);
        }
        baseRewardBox.gameObject.SetActive(false);
    }
}
