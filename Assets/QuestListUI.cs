using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;

/*
����Ʈ �̸� : questTitle
�󼼳��� : detailExplain
��ǥ Type : enum QuestType
 KillMonster,      // ���� óġ.
 GoToDestination,  // ������ ����
 ItemCollection, // ������ ����
 
GoalCount : 
	���� óġ�ô� ���� óġ��
	������ �����ô� ������ ������,
	
class Reward
{
ItemID
Count 
}*/

public enum QuestType
{
    KillMonster,      // ���� óġ.
    GoToDestination,  // ������ ����
    ItemCollection, // ������ ����
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
    /// ���� óġ�ô� ���� ID
    /// ������ �����ô� ������ ID,</summary>
    public int goalId;
    /// <summary>
    /// ���� óġ�ô� ���� óġ��
    /// ������ �����ô� ������ ������,</summary>
    public int goalCount;

    public List<RewardInfo> rewards;

    internal string GetGoalString()
    {
        switch (questType)
        {
            case QuestType.KillMonster: // �������� 5���� óġ�ϼ���.
                string monsterName = ItemDB.GetMosnterInfo(goalId).name;
                return $"{monsterName}�� {goalCount}���� ��������";
            case QuestType.GoToDestination: // ����Դ����� �̵��ϼ���.
                string destinationName = ItemDB.GetDestinationInfo(goalId).name;
                return $"{destinationName}�� ������";
            case QuestType.ItemCollection: // ������ 5�� �����ϼ���
                string itemName = ItemDB.GetItemInfo(goalId).name;
                return $"{itemName}�� {goalCount}�� �����ϼ���";
        }

        return "�ӽ� �۾��ؾ���";
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
        print($"{currentQuest.questTitle} ����Ʈ ������");
        UserData.Instance.questData.data.rejectIds
            .Add(currentQuest.id);

        ShowQuestList();
    }

    private void AcceptQuest()
    {
        print($"{currentQuest.questTitle} ����Ʈ ������");
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
        // ���ʿ� �ִ� ����Ʈ �̸� ����Ʈ �ʱ�ȭ
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


        // ���󸮽�Ʈ �ʱ�ȭ.
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


        // ���󸮽�Ʈ �ʱ�ȭ.
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
