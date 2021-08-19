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
����Ʈ �̸� : questTitle
�󼼳��� : detailExplain
��ǥ Type : enum QuestType
 KillMonster,      // ���� óġ.
 GoToDestination,  // ������ ����
 ItemCollection, // ������ ����
 
Count : 
	���� óġ�ô� ���� óġ��
	������ �����ô� ������ ������,
	
class Reward	
ItemID
Count
*/