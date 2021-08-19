using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestListUI : Singleton<QuestListUI>
{
    CanvasGroup canvasGroup;
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

/*
퀘스트 이름 : questTitle
상세내용 : detailExplain
목표 Type : enum QuestType
 KillMonster,      // 몬스터 처치.
 GoToDestination,  // 목적지 도착
 ItemCollection, // 아이템 수집
 
Count : 
	몬스터 처치시는 몬스터 처치수
	아이템 수집시는 아이템 수집수,
	
class Reward	
ItemID
Count
*/