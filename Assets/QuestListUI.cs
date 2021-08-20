using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class QuestListUI : Singleton<QuestListUI>
{
    CanvasGroup canvasGroup;

    public List<QuestInfo> questInfo;
    public QuestListBox questTitleBaseBox;
    public ItemInfoBox rewardIconBaseBox;
    void Start()
    {
        questTitleBaseBox = GetComponentInChildren<QuestListBox>();
        questTitleBaseBox.Init();
        questTitleBaseBox.gameObject.SetActive(false);

        rewardIconBaseBox = GetComponentInChildren<ItemInfoBox>();
        rewardIconBaseBox.Init();
        rewardIconBaseBox.gameObject.SetActive(false);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        selectedQuestTitle = transform.Find("Right/QuestTitle/SelectedQuestTitle/SelectedQuestTitle").GetComponent<Text>();
        rewardGoalExplain = transform.Find("Right/RewardGoal/RewardGoalExplain").GetComponent<Text>();
        questExplain = transform.Find("Right/QuestExplain").GetComponent<Text>();

        transform.Find("Down/YesButton").GetComponent<Button>().AddListener(this, AcceptQuest);
        transform.Find("Down/NoButton").GetComponent<Button>().AddListener(this, RejectQuest);
        
        transform.Find("CloseButton").GetComponent<Button>()
            .AddListener(this, CloseUI);
    }

    void AcceptQuest()
    {
        print($"{currentQuest.questTitle} 수락");
    }
    void RejectQuest()
    {
        print($"{currentQuest.questTitle} 거절");
    }

    List<GameObject> listItmes = new List<GameObject>();
    List<GameObject> rewardListItmes = new List<GameObject>();
    public void ShowQuestList()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        questTitleBaseBox.gameObject.SetActive(true);
        foreach (var item in questInfo)
        {
            var listItem = Instantiate(questTitleBaseBox, questTitleBaseBox.transform.parent);
            listItem.Init(item);
            listItem.GetComponent<Button>().onClick.AddListener(
                () => SelectItem(item));
            listItmes.Add(listItem.gameObject);
        }
        questTitleBaseBox.gameObject.SetActive(false);

        SelectItem(questInfo[0]);
    }

    Text selectedQuestTitle;
    Text questExplain;
    Text rewardGoalExplain;
    QuestInfo currentQuest;
    private void SelectItem(QuestInfo item)
    {
        currentQuest = item;
        selectedQuestTitle.text = item.questTitle;
        questExplain.text = item.detailExplain;
        rewardGoalExplain.text = item.GetItemGoalExplainString();

        rewardListItmes.ForEach(x => Destroy(x));

        rewardIconBaseBox.gameObject.SetActive(true);
        foreach (var rewardItem in item.rewards)
        {
            var listItem = Instantiate(rewardIconBaseBox, rewardIconBaseBox.transform.parent);
            listItem.Init(rewardItem);
            rewardListItmes.Add(listItem.gameObject);
        }
        rewardIconBaseBox.gameObject.SetActive(false);
    }

    public void CloseUI()
    {
        canvasGroup.DOFade(0, 0.5f)
            .OnComplete(OnCloseUI);
    }

    void OnCloseUI()
    {
        listItmes.ForEach(x => Destroy(x));
    }
}

[System.Serializable]
public class QuestInfo
{
    public int id;
    public string questTitle;
    [TextArea]
    public string detailExplain;
    public QuestType questType;
    /// <summary>
    /// 몬스터 처치시는 몬스터 ID
    /// 아이템 수집시는 아이템 ID
    /// </summary>
    public int goalID;
    /// <summary>
    /// 몬스터 처치시는 몬스터 처치수
    /// 아이템 수집시는 아이템 수집수
    /// </summary>
    public int goalCount;
    public List<RewardInfo> rewards;

    internal string GetItemGoalExplainString()
    {
        switch(questType)
        {
            case QuestType.KillMonster:
                string monsterName = ItemDB.GetMosngterInfo(goalID).monsterName;
                return $"{monsterName} 처치(0/{goalCount})";
            case QuestType.ItemCollection:
                string itemName = ItemDB.GetItemInfo(goalID).itemName;
                return $"{itemName} 수집(0/{goalCount})";
            case QuestType.GoToDestination:
                string destinationName = ItemDB.GetDestinationInfo(goalID).destinationName;
                return $"{destinationName}에 가기";
            default:
                Debug.LogError(questType + ": 에 대해서 퀘스트 설명 문장 작성하지 않았음");
                break;
        }

        return "";
    }
}
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
ItemID
Count
*/