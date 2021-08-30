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
    internal void Init(DeckStateType _deckState)
    {
        bgImage = transform.Find("Bg").GetComponent<Image>();

        deckState = _deckState;
        bgImage.sprite = deckState == DeckStateType.Enable ?
            enableSprite : disableSprite;
    }
}
