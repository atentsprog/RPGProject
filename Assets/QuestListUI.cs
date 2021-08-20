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
        print($"{currentQuest.questTitle} ����");
    }
    void RejectQuest()
    {
        print($"{currentQuest.questTitle} ����");
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
    /// ���� óġ�ô� ���� ID
    /// ������ �����ô� ������ ID
    /// </summary>
    public int goalID;
    /// <summary>
    /// ���� óġ�ô� ���� óġ��
    /// ������ �����ô� ������ ������
    /// </summary>
    public int goalCount;
    public List<RewardInfo> rewards;

    internal string GetItemGoalExplainString()
    {
        switch(questType)
        {
            case QuestType.KillMonster:
                string monsterName = ItemDB.GetMosngterInfo(goalID).monsterName;
                return $"{monsterName} óġ(0/{goalCount})";
            case QuestType.ItemCollection:
                string itemName = ItemDB.GetItemInfo(goalID).itemName;
                return $"{itemName} ����(0/{goalCount})";
            case QuestType.GoToDestination:
                string destinationName = ItemDB.GetDestinationInfo(goalID).destinationName;
                return $"{destinationName}�� ����";
            default:
                Debug.LogError(questType + ": �� ���ؼ� ����Ʈ ���� ���� �ۼ����� �ʾ���");
                break;
        }

        return "";
    }
}
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
ItemID
Count
*/