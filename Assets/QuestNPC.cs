using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        TalkAlertUI.Instance.ShowText(@"�����ھ� �����!
�Ҹ��� �־�(Q)");
    }
}
