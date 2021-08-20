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
