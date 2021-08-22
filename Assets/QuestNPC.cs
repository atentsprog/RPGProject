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
        print("퀘스트 목록 UI푯기 하자.");
        CharacterTextBoxUI.Instance.CloseUI();
        QuestListUI.Instance.ShowQuestList(questIds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        print(other);

        // 유저에게 보여줄 퀘스트가 있을때만 진행하자
        // 보여줄 퀘스트 : 수락/완료/거절한 퀘스트를 제외
        if (HaveUseableQuest() == false)
            return;

        questAcceptKey.Enable();
        CharacterTextBoxUI.Instance.ShowText("모험자야 멈춰봐!\n할말이 있어(Q)");
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

        CharacterTextBoxUI.Instance.CloseUI();
    }
}
