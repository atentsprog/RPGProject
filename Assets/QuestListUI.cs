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

public class QuestListUI : Singleton<QuestListUI>
{
    CanvasGroup canvasGroup;
    List<QuestInfo> quests;
    QuestTitleBox baseQuestTitleBox;
    RewardBox baseRewardBox;
    List<GameObject> questTitleBoxs = new List<GameObject>();
    List<GameObject> rewardBoxs = new List<GameObject>();

    Text selectedQuestTitle;
    Text goalExplain;
    Text questExplain;
    Text accectQuestText;
    void Start()
    {
        selectedQuestTitle = transform.Find("Right/QuestTitle/SelectedQuestTitle/SelectedQuestTitle").GetComponent<Text>();
        goalExplain = transform.Find("Right/Goal/GoalExplain").GetComponent<Text>();
        questExplain = transform.Find("Right/QuestExplain").GetComponent<Text>();
        accectQuestText = transform.Find("AccectQuest/AccectQuestText").GetComponent<Text>();

        transform.Find("AccectQuest/Yes").GetComponent<Button>().onClick
                .AddListener(() => AcceptQuest());
        transform.Find("AccectQuest/No").GetComponent<Button>().onClick
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
        canvasGroup.DOFade(0, 0.5f).SetUpdate(true)
            .OnComplete(() =>
            {
                StageManager.GameState = GameStateType.Play;
                gameObject.SetActive(false);
            });
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

    public void ShowQuestList(List<int> questIds = null)
    {
        if (canvasGroup.alpha > 0) //gameObject.activeInHierarchy
            return;
        gameObject.SetActive(true);
        StageManager.GameState = GameStateType.Menu;

        if (questIds != null)
            quests = ItemDB.Instance.GetQuestInfo(questIds);// 
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetUpdate(true);

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

        baseQuestTitleBox.gameObject.SetActive(false);
        baseRewardBox.gameObject.SetActive(false);
    }

    QuestInfo currentQuest;
    private void OnClickTitleItem(QuestInfo item)
    {
        currentQuest = item;
        accectQuestText.text = $"{item.questTitle} 퀘스트를 수락 하시겠습니까?";
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
