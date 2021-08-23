using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    public InputAction showUIKey = new InputAction("Key", InputActionType.Button, "<Keyboard>/q");
    public string speechString = "모험자야 멈춰봐!\n할말이 있어(Q)";
    public string npcName = "상점 NPC";
    public string npcSpriteName = "NPC1";

    private void Awake() => showUIKey.performed += QuestAcceptKey_performed;
    private void QuestAcceptKey_performed(InputAction.CallbackContext obj)
    {
        print("UI표시 하자.");

        CharacterTextBoxUI.Instance.CloseUI();
        ShowUI();
    }

    protected virtual void ShowUI()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        print(other);

        // 유저에게 보여줄 퀘스트가 있을때만 진행하자
        // 보여줄 퀘스트 : 수락/완료/거절한 퀘스트를 제외
        if (IsUseableMenu() == false)
            return;

        showUIKey.Enable();
        CharacterTextBoxUI.Instance.ShowText(speechString
            , 3, npcName, npcSpriteName);
    }

    //protected virtual bool IsUseableMenu() { return true; }
    protected virtual bool IsUseableMenu() => true;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        showUIKey.Disable();

        CharacterTextBoxUI.Instance.CloseUI();
    }
}

public class QuestNPC : NPC
{
    public List<int> questIds = new List<int>();
    protected override bool IsUseableMenu()
    {
        List<int> ignoreIds = new List<int>();
        ignoreIds.AddRange(UserData.Instance.questData.data.acceptIds);
        ignoreIds.AddRange(UserData.Instance.questData.data.rejectIds);

        return questIds.Where(x => ignoreIds.Contains(x) == false).Count() > 0;
    }


    protected override void ShowUI()
    {
        QuestListUI.Instance.ShowQuestList(questIds);
    }
}
