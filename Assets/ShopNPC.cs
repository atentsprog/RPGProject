﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC
{
    protected override void ShowUI()
    {
        print("샵 ui표시)");
        ShopUI.Instance.ShowUI();
    }
}
