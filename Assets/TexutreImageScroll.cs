using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexutreImageScroll : MonoBehaviour
{
    float curX = 0;
    float curY = 0;
    public Vector2 amount;
    new MeshRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        curX += amount.x;
        curY += amount.y;
        renderer.sharedMaterial.mainTextureOffset = new Vector2(curX, curY);
    }
}
