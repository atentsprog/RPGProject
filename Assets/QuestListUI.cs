using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

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

        questTitleBoxs.ForEach(x => Destroy(x));
        questTitleBoxs.Clear();
        // ���ʿ� �ִ� ����Ʈ �̸� ����Ʈ �ʱ�ȭ
        baseQuestTitleBox.gameObject.SetActive(true);
        foreach (var item in quests)
        {
            var titleItem = Instantiate(baseQuestTitleBox, baseQuestTitleBox.transform.parent);
            titleItem.Init(item);
            titleItem.GetComponent<Button>().onClick
                .AddListener(() => OnClickTitleItem(item));
            questTitleBoxs.Add(titleItem.gameObject);
        }
        baseQuestTitleBox.gameObject.SetActive(false);

        OnClickTitleItem(quests[0]);
    }
    private void OnClickTitleItem(QuestInfo item)
    {
        selectedQuestTitle.text = item.questTitle;
        questExplain.text = item.detailExplain;
        goalExplain.text = item.GetGoalString();


        // ���󸮽�Ʈ �ʱ�ȭ.
        rewardBoxs.ForEach(x => Destroy(x));
        rewardBoxs.Clear();
        baseRewardBox.gameObject.SetActive(true);
        foreach (var item in item.rewards)
        {
            var titleItem = Instantiate(baseRewardBox, baseRewardBox.transform.parent);
            titleItem.Init(item);
            rewardBoxs.Add(titleItem.gameObject);
        }
        baseRewardBox.gameObject.SetActive(false);
    }
}
