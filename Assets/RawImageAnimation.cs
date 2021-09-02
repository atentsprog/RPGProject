using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageAnimation : MonoBehaviour
{
    [SerializeField] float waitSecond = 0.1f;
    public int xCount = 4;
    public int yCount = 5;
    public Vector2 offset;
    IEnumerator Start()
    {
        RawImage rawImage = GetComponent<RawImage>();
        var rect = rawImage.uvRect;
        float width = (1f - offset.x) / xCount; 
        float height = (1f - offset.y) / yCount;
        int number = 1;
        int totalCount = xCount * yCount;
        WaitForSeconds waitForSecond = new WaitForSeconds(waitSecond);
        while (true)
        {
            int numberPerLine = number++ % totalCount;
            rect.x = offset.x + width * (numberPerLine % xCount);
            rect.y = offset.y + height * (numberPerLine / xCount);
            rawImage.uvRect = rect;
            yield return waitForSecond;
        } 
    }
}
