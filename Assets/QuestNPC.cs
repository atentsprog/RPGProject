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
        print("í€˜ìŠ¤íŠ¸ ëª©ë¡ UIí‘œì‹œ í•˜ì.");
        QuestListUI.Instance.ShowQuestList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;
        print(other);

        // À¯Àú¿¡°Ô º¸¿©ÁÙ Äù½ºÆ®°¡ ÀÖÀ»¶§¸¸ ÁøÇàÇÏÀÚ
        // º¸¿©ÁÙ Äù½ºÆ® : ¼ö¶ô/¿Ï·á/°ÅÀıÇÑ Äù½ºÆ®¸¦ Á¦¿Ü
        if (HaveUseableQuest() == false)
            return;

        questAcceptKey.Enable();
        TalkAlertUI.Instance.ShowText("ì´ë³´ê²Œ ì—¬í–‰ìì—¬ ê¸°ë‹¤ë ¤ë³´ê²Œ\n ìë„¤ì—ê²Œ í• ë§ì´ ìˆë‹¤ë„¤..(Q)");
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
