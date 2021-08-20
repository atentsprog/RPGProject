using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public int GoalId;
    /// <summary>
    /// ���� óġ�ô� ���� óġ��
    /// ������ �����ô� ������ ������,</summary>
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

        // ���ʿ� �ִ� ����Ʈ �̸� ����Ʈ �ʱ�ȭ
        baseQuestTitleBox.gameObject.SetActive(true);
        foreach (var item in quests)
        {
            var titleItem = Instantiate(baseQuestTitleBox, baseQuestTitleBox.transform);
            titleItem.Init(item);
        }
        baseQuestTitleBox.gameObject.SetActive(false);
    }
}
