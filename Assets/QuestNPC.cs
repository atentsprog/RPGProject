using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other);
        TalkAlertUI.Instance.ShowText(@"모험자야 멈춰봐!
할말이 있어(Q)");
    }
}
