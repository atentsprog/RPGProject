using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestNPC : NPC
{ 
    public List<int> questIds = new List<int>();

    protected override void ShowUI()
    {
        QuestListUI.Instance.ShowQuestList(questIds);
    }

    protected override bool IsUseableMenu()
    {
        List<int> ignoreIds = new List<int>();
        ignoreIds.AddRange(UserData.Instance.questData.data.acceptIds);
        ignoreIds.AddRange(UserData.Instance.questData.data.rejectIds);

        return questIds.Where(x => ignoreIds.Contains(x) == false).Count() > 0;
    }
}

public abstract class NPC : MonoBehaviour
{
    [TextArea]
    public string speechString = "모험자야 멈춰봐!\n할말이 있어(Q)";
    public string npcName = "NPC";
    public string npcPortrait = "NPC1";
    public InputAction questAcceptKey = new InputAction(
        "Q Key", InputActionType.Button, "<Keyboard>/q");

    private void Awake() => questAcceptKey.performed += ShowUiKey_performed;
    private void ShowUiKey_performed(InputAction.CallbackContext obj)
    {
        // 마우스 커서 보이게 하자.
        Cursor.lockState = CursorLockMode.None;
        print("UI표시 하자.");
        CharacterTextBoxUI.Instance.CloseUI();
        ShowUI();
    }

    abstract protected void ShowUI();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        print(other);

        // 유저에게 보여줄 퀘스트가 있을때만 진행하자
        // 보여줄 퀘스트 : 수락/완료/거절한 퀘스트를 제외
        if (IsUseableMenu() == false)
            return;

        questAcceptKey.Enable();
        CharacterTextBoxUI.Instance.ShowText(speechString, 3, npcName, npcPortrait);
    }

    virtual protected bool IsUseableMenu() => true;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        questAcceptKey.Disable();

        CharacterTextBoxUI.Instance.CloseUI();
    }
}
