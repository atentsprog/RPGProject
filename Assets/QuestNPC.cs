using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestNPC : MonoBehaviour
{
    public InputAction questAcceptKey;
    public List<int> questIds = new List<int>();

    private void Awake() =>questAcceptKey.performed += QuestAcceptKey_performed;
    private void QuestAcceptKey_performed(InputAction.CallbackContext obj)
    {
        print("퀘스트 목록 UI표시 하자.");
        QuestListUI.Instance.ShowQuestList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        print(other);

        // �������� ������ ����Ʈ�� �������� ��������
        // ������ ����Ʈ : ����/�Ϸ�/������ ����Ʈ�� ����
        if (HaveUseableQuest() == false)
            return;

        questAcceptKey.Enable();
        TalkAlertUI.Instance.ShowText("이보게 여행자여 기다려보게\n 자네에게 할말이 있다네..(Q)");
    }

    private bool HaveUseableQuest()
    {
        List<int> ignoreIds = new List<int>();
        ignoreIds.AddRange(UserData.Instance.questData.data.acceptIds);
        ignoreIds.AddRange(UserData.Instance.questData.data.rejectIds);

        return questIds.Where(x => ignoreIds.Contains(x) == false).Count() > 0;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        questAcceptKey.Disable();
    }
}
