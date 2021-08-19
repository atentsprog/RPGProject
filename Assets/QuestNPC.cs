using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestNPC : MonoBehaviour
{
    public InputAction questAcceptKey;

    private void Awake() =>questAcceptKey.performed += QuestAcceptKey_performed;
    private void QuestAcceptKey_performed(InputAction.CallbackContext obj)
    {
        print("����Ʈ ��� UIǩ�� ����.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        //print(other); 
        questAcceptKey.Enable();
        TalkAlertUI.Instance.ShowText("�����ھ� �����!\n�Ҹ��� �־�(Q)");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        questAcceptKey.Disable();
    }
}
