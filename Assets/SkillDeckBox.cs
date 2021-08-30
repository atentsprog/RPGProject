using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDeckBox : MonoBehaviour
{
    [SerializeField] Sprite enableSprite;
    [SerializeField] Sprite disableSprite;

    Image bgImage;

    DeckStateType deckState;

    Text skillName;
    Text level;
    Image icon;
    internal void Init(DeckStateType _deckState)
    {
        bgImage = transform.Find("Bg").GetComponent<Image>();
        skillName = transform.Find("SkillName").GetComponent<Text>();
        level = transform.Find("Level").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        skillName.enabled = false;
        level.enabled = false;
        icon.enabled = false;

        deckState = _deckState;
        bgImage.sprite = deckState == DeckStateType.Enable ?
            enableSprite : disableSprite;
    }
}
