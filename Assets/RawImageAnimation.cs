using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageAnimation : MonoBehaviour
{
    //public int xCount = 4;
    //public int yCount = 5;
    //public float speed = 0.1f;

    //RawImage rawImage;
    //IEnumerator Start()
    //{
    //rawImage = GetComponent<RawImage>();
    //float width = 1f / xCount; // 0.25
    //float height = 1f / yCount;
    //var rect = rawImage.uvRect;
    //rect.width = width;
    //rect.height = height;
    //rect.x = rect.y = 0;
    //rawImage.uvRect = rect;

    //WaitForSeconds waitSecond = new WaitForSeconds(speed);

    //int number = 1;
    //while (true)
    //{
    //    yield return waitSecond;
    //    rect.x = width * number;
    //    rect.y = height * number;
    //    //xCount
    //    //yCount

    //    rawImage.uvRect = rect;
    //    number++;
    //}
    //}

    [SerializeField] float waitSecond = 0.1f;
    float xCount = 4;
    float yCount = 5;
    IEnumerator Start()
    {
        RawImage rawImage = GetComponent<RawImage>();
        var rect = rawImage.uvRect;
        float width = 1f / xCount; // 0.25
        float height = 1f / yCount;
        while (true)
        {
            for (int y = 0; y < yCount; y++)
            {
                rect.y = 1 - ((y + 1) * height);
                for (int x = 0; x < xCount; x++)
                {
                    rect.x = x * width;
                    //print($"rect : {rect}");
                    rawImage.uvRect = rect;
                    yield return new WaitForSeconds(waitSecond);
                }
            }
        }
    }
}
